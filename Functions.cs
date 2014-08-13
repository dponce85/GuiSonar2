using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiSonar2
{
    public partial class Form1
    {
        float[] filter(float[] B, float[] A, float[] xk)
        {
            int N = 4;
            float[] NumCoeff = B;
            float[] DenCoeff = A;
            float[] Signal = new float[xk.Length];
            Signal = xk;
            float[] FilteredSignal = new float[xk.Length];
            float[] Reg = new float[100];
            int NumSigPts;

            for (int i = 0; i < xk.Length; i++)
            { FilteredSignal[i] = 0; }

            for (int i = 0; i < 100; i++)
            { Reg[i] = 0; }

            NumSigPts = Signal.Length;

            for (int j = 0; j < NumSigPts; j++)
            {
                for (int k = N; k >= 1; k--)
                { Reg[k] = Reg[k - 1]; }

                // El denominador
                Reg[0] = Signal[j];

                for (int k = 1; k <= N; k++)
                { Reg[0] = Reg[0] - DenCoeff[k] * Reg[k]; }

                // El numerador
                float y = 0;
                for (int k = 0; k <= N; k++)
                {
                    y = y + NumCoeff[k] * Reg[k];
                    FilteredSignal[j] = y;
                }
            }

            /*
            ff1 = fft(Signal);
            figure, plot(abs(ff1(1:end/2)));
            ff2 = fft(FilteredSignal);
            figure, plot(abs(ff2(1:end/2)));
            */
            return FilteredSignal;
        }

        float[][] envolvente1(float[] x, int N, float fc, float fs)
        {
            float[][] r = new float[2][];
            r[1] = new float[1];
            int M;
            float[] y;
            float[] hb = Fir1Coeff;
            float av;
            M = (int)(fs / (2 * fc));
            //hb = Fir1(N, 2 * fc / fs);

            for (int i = 0; i < x.Length; i++)
                if (x[i] < 0) x[i] = 0;

            //mean:
            av = x.Average();
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = x[i] - av;
            }
            y = conv(x, hb, ConvMethod.full);
            float[] y2 = new float[y.Length / M];
            for (int i = 0,j=0; i < y.Length; i = i + M,j++)
            {
                y2[j] = y[i];
            }

            av = y2.Average();
            for (int i = 0; i < y2.Length; i++)
            {
                y2[i] = y2[i] - av;
            }

            r[0] = y2;
            r[1][0] = fs / M;

            return r;
        }

        float[][] envolventeban(float[] B51k, float[] B52k, float[] B53k, float[] B54k, float[] B55k, float[] B56k, float[] B57k, float[] B58k, int ord, float fc1, float fs1)
        {
            float av;
            float[][] B5 = new float[9][];
            B5[8] = new float[1];
            B5[0] = envolvente1(B51k, ord, fc1, fs1)[0];
            B5[1] = envolvente1(B52k, ord, fc1, fs1)[0];
            B5[2] = envolvente1(B53k, ord, fc1, fs1)[0];
            B5[3] = envolvente1(B54k, ord, fc1, fs1)[0];
            B5[4] = envolvente1(B55k, ord, fc1, fs1)[0];
            B5[5] = envolvente1(B56k, ord, fc1, fs1)[0];
            B5[6] = envolvente1(B57k, ord, fc1, fs1)[0];
            B5[7] = envolvente1(B58k, ord, fc1, fs1)[0];
            B5[8][0] = envolvente1(B58k, ord, fc1, fs1)[1][0];

            for (int j = 0; j < 8; j++)
            {
                av = B5[j].Average();
                for (int i = 0; i < B5[0].Length; i++)
                { B5[j][i] = B5[j][i] - av; }
            }

            return B5;
        }


        float[][] decima(float[][] BEN, float f0, float fsb, float sensi, float[] ct, float[] FAV, float boc, float tipve)
        {
            float[][] r = new float[6][];
            float[] B;
            float[] h=new float[401];
            float av;
            float[] vb;
            float fs1 = 2 * 10 * f0;
            int M = (int)Math.Round(fsb / fs1);
            int[] ip;
            float[] fre;
            float k;
            float pote = 0;
            float[] FJ;
            int NNF;
            
            float[] FJA=new float[0];
            float[] FJApos;
            float delta1;
            float f01;
            int k1;
            int pos;

            if (M > 1)
            { h = FIR400; }//fir1_400(400, 1.0f / M); }
            else
            { fs1 = fsb; }

            ip = find(ct, sensi, FindMethod.Less);

            k = 1;

            pote = (float)Math.Ceiling(Math.Log((BEN[0].Length * BEN.Length), 2.0f));
            NNF = (int)Math.Pow(2.0, pote);
            FJ = new float[2 * NNF];// FJ
            fre = new float[2 * NNF];//fre

            for (int u = 0; u < ip.Length; u++)
            {
                B = new float[BEN[0].Length];
                for (int f = 0; f < BEN[0].Length; f++)
                {
                    
                    B[f] = BEN[ip[u]][f];
                }

                if (M > 1)
                {
                    B = filter3(B,h);
                    //PlotVar(B,"B=filter3", true);
                    int i = 0;
                    Array.Resize(ref B, B.Length / M);
                    while (i < B.Length / M)
                    {
                        B[i] = B[i * M];
                        i++;
                    }
                }
                //PlotVar(B,"B=filter3", true);
                pote = (float)Math.Ceiling(Math.Log((BEN[0].Length*BEN.Length), 2.0f));
                NNF = (int)Math.Pow(2.0, pote);
                Array.Resize(ref FJ, 2 * NNF);
                Array.Resize(ref fre, 2 * NNF);
                               
                av = B.Average();
                for (int i = 0; i < B.Length; i++)
                { B[i] = B[i] - av; }
                /////////////////////////////////
                if (tipve == 1)
                {
                    vb = new float[B.Length];
                    for (int i = 0; i < B.Length; i++)
                    {
                        vb[i] = 1;
                    }
                }
                else if (tipve == 2)
                {
                    vb = hann(B.Length);
                }
                else
                {
                    vb = blackman(B.Length);
                }
                // /////////////////
                /* [FJ,fre]=freqz(B.*vb,1,2*NNF,fs1);
                  FJ=abs(FJ);
                  FJ=FJ.*FJ; */
                double[] tempQZ= new double[B.Length];
                double[] preFJ= new double[4*NNF];

                for (int i = 0; i < B.Length; i++)
                {   tempQZ[i] = B[i] * vb[i];  }

                Fourier.FFT(tempQZ, preFJ);
                FJ = moduloArrayComplejo(preFJ, preFJ.Length);
               //FJ = freqz(tempQZ,1,2*NNF,fs1);
               //FJ=FJ.*FJ
               // FJ = new float[pruFJ.Length];
                /*for (int i = 0; i < pruFJ.Length; i++)
                { FJ[i] = pruFJ[i]; }
                */
                

                fre = new float[2 * NNF]; // fre
                for (int i = 0; i < (2 * NNF); i++)
                {
                    fre[i] = (fs1 / 2.0f) * (i / (2.0f * NNF));
                }

                ///////////////////
                FJA = new float[FJ.Length];
                
                if (u == 0)
                {
                    for (int i = 0; i < FJ.Length; i++)
                    {
                        FJA[i] = FJ[i];
                    }
                }

                if (u > 0)
                {
                    for (int i = 0; i < FJ.Length; i++)
                    {
                        FJA[i] = FJ[i] + FJA[i];
                    }

                    k = k + 1;

                }

            }

            for (int i = 0; i < FJA.Length; i++)
            {
                FJA[i] = FJA[i] / k;
            }

            if (boc == 1 && (FAV.Length == FJA.Length))
            {
                for (int i = 0; i < FAV.Length; i++)
                {
                    FJA[i] = (FAV[i] + FJA[i]) / 2;
                }
            }

            delta1 = fre[1];
            k1 = (int)Math.Round(0.5 / delta1);

            for (int i = 0; i < k1; i++)
            {
                FJA[i] = 0;
            }

            //FJApos = FJA;
            FJApos = new float[FJA.Length];
            for (int i = 0; i < FJA.Length; i++)
            {
                FJApos[i] = FJA[i];
            }

            for (int i = 0; i < FJA.Length; i++)
            {
                if (FJApos[i] < 0)
                { FJApos[i] = -FJApos[i]; }
            }

            pos = indexMax(FJApos);
            f01 = fre[pos];
            boc = 1;

            r[0] = FJA;
            r[1] = new float[1];
            r[1][0] = f01;
            r[2] = fre;
            r[3] = new float[1];
            r[3][0] = delta1;
            r[4] = new float[1];
            r[4][0] = fs1;
            r[5] = new float[1];
            r[5][0] = boc;
            return r;
        }        


        void dwt(float[] X, float[] hw0, float[] gw0, out float[] Xlp, out float[] Xhp)
        {
            Xlp = subSamp(conv(X, hw0), 2);
            Xhp = subSamp(conv(X, gw0), 2);
        }

        float[] subSamp(float[] X, int step)
        {
            int retLen = X.Length / step;
            float[] ret = new float[retLen];
            for (int i = 1, j = 0; i < X.Length; i += step)
                ret[j++] = X[i];

            return ret;
        }

        float[] double2float(double[] x)
        {
            float[] tmp = new float[x.Length];

            tmp = Array.ConvertAll(x, element => (float)element);

            return tmp;

        }

        float[][] detecarm33(float[] FV, float f0, float delta, float[] f, float sensar)
        {
            float[][] r = new float[7][];
            float[] FV2 = new float[FV.Length];
            float[] FV1;

            float fmin = 0;
            float ma;
            float ik1, ik2, k1, k2;
            float lim1;
            float[] k = new float[2];
            //float[] mak = new float[2];
            float ban = 0;
            int p1, p2;
            int nar = 0;
            int L = 0;
            int pos = 0;
            int tol;
            int Na;
            //int[] ivj = new int[2];
            int[] ivj1 ;
            float j0;
            int pp;
            float[]  mkk;
            float[] FC;
            float[] FD;
            float[] por;
            float[] makf;
            float[] makfpor;
            float[] frecarm;
            float f00;
            List<int> ikk1 = new List<int>();
            List<int> ivj = new List<int>();
            List<float> mak = new List<float>();
            List<int> ivv2 = new List<int>();
           
            List<float> mak1 = new List<float>();

                      
            nar = 0;
            fmin = 0.5f;
            k1 = (int)Math.Round(fmin / delta);

            //FV1 = FV;
            FV1 = new float[FV.Length];
            for (int i = 0; i < FV.Length; i++)
            { FV1[i] = FV[i];  }

            //FV1(1:K1) = 0;
            for (int i = 0; i < k1; i++)
            { FV1[i] = 0;  }

            L = FV1.Length;

            ma = FV1.Max();
            pos = indexMax(FV1);
            
            PicoRet pRet = pico(FV1, pos, L);
            ik1 = pRet.ik1;
            ik2 = pRet.ik2;
            k1 = pRet.k1;
            k2 = pRet.k2;

            tol = (int)Math.Round((ik2 - ik1 )/2.0f);
           k[0] = k1;
           k[1] = k2;
           lim1 = k.Min();
           p1 = pos - tol;
           p2 = pos + tol;

           if (p1 < 0)
           { p1 = 0; }

           if (p2 > L)
           { p2 = L-1; }

           //FV1(p1:p2) = 0  
           for (int i = p1; i <= p2; i++)
           {
               FV1[i] = 0;
           }

           Na = 10;
           
           ivj.Add(pos);
           mak.Add(ma);

           ban = 0;
           cont = 1;
           

           while (ban == 0)
           { 
             ma = FV1.Max();    
             pos = indexMax(FV1);

             ivj.Insert(ivj.Count, pos);
             mak.Insert(mak.Count, ma);
             cont=cont+1;
      
              if( cont==Na)
              { ban=1;} 
                        
              p1=pos-tol;
              p2=pos+tol;
              if( p1<0)
              { p1=0; }
             
              if( p2>(L-1))
              { p2=(L-1);}
                 
              //FV1(p1:p2)=0;
               for( int i=p1;i<=p2;i++)
               {
                 FV1[i] = 0;
               }
               
              if(FV1.Sum()==0)
              { ban=1;}
           
            }//fin de ban==0


            ivj1=new int[ivj.Count];//ivj1 = ivj
            for (int i = 0; i < ivj.Count; i++)
            { ivj1[i] = ivj[i]; }
                //ikk1=[];
                //mak1=[];
                for (int u = 0; u < ivj1.Length; u++)
                {
                    ma = ivj1.Max();
                    pos = indexMax(ivj1);
                    ikk1.Insert(0, (int)ma); //ikk1=[ma ikk1];  
                    if (mak1.Count == 0) mak1.Add(mak[pos]);
                    else mak1.Insert(mak1.Count, mak[pos]); //mak1=[mak1 mak(pos)]; 
                    ivj1[pos] = 0;
                }    
            
            //ikk1
            float[]  v = new float[ikk1.Count];
            int[][] zz = new int[Na][];
            int[] sar;
            float[]  bak = new float[ikk1.Count];
            int posm;
            int posp1;
            float posp;
            float nr;
            int ne;
            int nd;
            for (int i = 0; i < Na; i++)
            {
                zz[i] = new int[ikk1.Count];
            }

                for (int u = 0; u < ikk1.Count; u++)
                {
                    j0 = f[ikk1[u]];
                    ban = 0;
                    pp = 1;//contador
                    mkk = new float[mak1.Count];
                    mkk = mak1.ToArray();

                    while (pp <= mkk.Length)
                    {
                        //ma = mkk.Max();
                        posm = indexMax(mkk);
                        mkk[posm] = 0;
                        posp1 = ikk1[posm];
                        posp = f[ikk1[posm]];
                        nr = posp / j0;
                        ne = (int)Math.Floor(nr);
                        nd = (int)Math.Floor((nr - ne) * 10);
                        if ((ne >= 1 && nd == 9) || (ne >= 1 && nd == 0))
                        {   
                            zz[(int)v[u]][u] = posp1;
                            bak[u] = bak[u] + FV[posp1];
                            v[u] = v[u] + 1;
                            
                        }
                        pp = pp + 1;
                    }
                }    

            pos = indexMax(bak);

            int[] ivv = new int[Na];
            
            for(int i=0;i<Na;i++)
            { ivv[i] = zz[i][pos]; }
             
            int[] ip1;
            
            ip1 = find(ivv,0,FindMethod.Greater);
            int[] iv2=new int[ip1.Length];
            for(int i=0; i< ip1.Length ;i++ )
            {
              iv2[i] = ivv[ip1[i]];
            }
            //iv2

            Array.Resize(ref ivj1,iv2.Length);
            //ivj1=iv2;
            ivj1 = new int[iv2.Length];
            for(int i = 0; i < iv2.Length; i++)
            { ivj1[i] = iv2[i]; }
              ikk1.Clear(); // ikk1=[];
              mak1.Clear(); //mak1=[];
            
            
            for(int u= 0 ;u< ivj1.Length ;u++)
            {  
                ma = ivj1.Max();
                pos = indexMax(ivj1);
                
                //if (ikk1.Count == 0)
                 //   ikk1.Add((int)ma);
               // else
                ikk1.Insert(0, (int)ma);
                mak1.Insert(mak1.Count,mak[pos]);
                ivj1[pos] = 0;
                   
            }  
            
            Array.Resize(ref iv2,ikk1.Count);
            iv2=ikk1.ToArray();
            float[] fg=new float[iv2.Length];
            for(int i=0;i<iv2.Length ;i++)
            {
                fg[i]=f[iv2[i]];
            }
            
            sar = new int[fg.Length];
            
            for(int i=0;i<fg.Length;i++)
            {
              sar[i] = (int)Math.Round(fg[i]/fg[0]);
            }
   
            int j=0;
            int[] ip;
            int[] sar2 = new int[sar.Length];
            int[] sar_ip;
            List<int> ivvsar = new List<int>();
            while (j<sar.Length)
            {     
                for(int i=0;i<sar2.Length;i++)
                {sar2[i] = sar[i] - sar[j];}
                ip=find(sar2,0,FindMethod.Equal);
                sar_ip = new int[ip.Length]; 
                for(int i=0;i<ip.Length;i++)
                {
                  sar_ip[i] = sar[ip[i]];
                }
                pos = indexMax(sar_ip);
                j=j+ip.Length;
                if (ivvsar.Count == 0) ivvsar.Add(iv2[ip[pos]]);
                else ivvsar.Insert(ivvsar.Count, iv2[ip[pos]]);
                
            } 
  
           
            //iv2=[]; 
            //iv2=ivv;       
            Array.Resize(ref iv2,ivvsar.Count);
            
            for (int i = 0; i < ivvsar.Count; i++)
            { iv2[i] = ivvsar[i]; }            

            //iv2

            if( iv2.Length >1 )
              { 
               FC = new float[FV.Length];//FC=zeros(length(FV),1);
               for(int i = 0;i< iv2.Length; i++)               
               {
               FC[iv2[i]]=FV[iv2[i]];
               }
               FD = new float[iv2.Length];
               for(int i=0;i<iv2.Length ;i++)
               {
                 FD[i]=FV[iv2[i]];     
               } 
               
               pos=indexMax(FD);
               FD[pos]=0;
               ma=FD.Max();
               por=new float[FC.Length]; 
               for(int i=0; i<FC.Length;i++)
               {
		       por[i]=(FC[i]*100.0f)/(float)ma;	
               } 
               
               
               int[] iv2_pre;
               iv2_pre = find(por,sensar,FindMethod.GreaterEqual);
               Array.Resize(ref iv2,iv2_pre.Length);
               //iv2=find(por>=sensar);
             }   

            Array.Resize(ref fg, iv2.Length);
              for(int i=0;i<iv2.Length;i++)
              {
                fg[i]=f[iv2[i]];
              }


              Array.Resize(ref sar, fg.Length);
              for (int i = 0; i < fg.Length; i++)
              {
                  sar[i] = (int)Math.Round(fg[i] / fg[0]);
              }
              makf = new float[iv2.Length];
              for (int i = 0; i < iv2.Length; i++)
              {
                  makf[i] = FV[iv2[i]];
              }
              //makf=floor(makf*100000)/100000;
              for (int i = 0; i < makf.Length; i++)
              {
                  makf[i] = (float)Math.Floor(makf[i] * 100000) / 100000.0f;
              }

              for (int i = 0; i < makf.Length; i++)
              {
                  makf[i] = ((float)Math.Floor(makf[i] * 100000))/100000.0f;
              }
              //makfpor = makf * 100 / max(makf);
              makfpor = new float[makf.Length];
              for (int i = 0; i < makf.Length; i++)
              {
                  makfpor[i] = ((float)Math.Floor(makf[i] * 100))/ makf.Max();
              }

              //makfpor=floor(makfpor*100000)/100000;
              for (int i = 0; i < iv2.Length; i++)
              {
                  makfpor[i] = ((float)Math.Floor(makfpor[i]*100000))/100000.0f;
              }
              // FV2=zeros(length(FV),1);

              for (int i=0; i < iv2.Length; i++)
              {
                  FV2[iv2[i]] = FV[iv2[i]];
              }
                  
              nar=iv2.Length;
              frecarm = new float[iv2.Length];

              for (int i = 0; i < iv2.Length; i++)
              {
                  frecarm[i] =((float)Math.Floor((iv2[i]*delta*60)*100000))/100000.0f;
              }

              //f00=min(frecarm);
              //if (frecarm.Length > 1)
              //    f00 = frecarm.Min();
             // else
              f00 = frecarm.Min();
              r[0] = FV2;
              r[1] = new float[1];
              r[1][0] = f00;
              r[2] = new float[1];
              r[2][0] = nar;
              r[3] = frecarm;
              r[4]= Array.ConvertAll(sar, element => (float)element);
              r[5] = makf;
              r[6] = makfpor;
           return r;
        }
            float[] fir400(float Wn)
        {
            

            float PI = (float)Math.PI;
            int L = 400; /* Filter length */
            int Pr_L = L;

            int N = L - 2;

            int odd, i, j, nhlf, i1;
            float f1, gain, c1;

            float[] wind = new float[Pr_L];
            float[] xn = new float[Pr_L / 2];
            float[] b = new float[Pr_L / 2];
            float[] c = new float[Pr_L / 2];
            float[] c3 = new float[Pr_L / 2];
            float[] bb = new float[Pr_L];


            gain = 0.0f;
            // N = N + 1;
            odd = N - (N / 2) * 2; /* odd = rem(N,2) */

            /*wind = hamming(N);*/
            for (i = 0; i < Pr_L; i++)
            {
                wind[i] = (float)(0.54 - 0.46 * Math.Cos((2 * PI * i) / (N - 1)));
            }

            f1 = Wn / 2.0f;
            c1 = f1;
            nhlf = N / 2; // (N + 1) / 2;
            i1 = odd + 1;

            /* Lowpass */

            if (odd != 0)
                b[0] = 2 * c1;

            for (i = 0; i < nhlf; i++)
            {
                xn[i] = i + 0.5f * (1 - odd);
            }

            for (i = 0; i < nhlf; i++)
            {
                c[i] = PI * xn[i];
            }

            for (i = 0; i < nhlf; i++)
            {
                c3[i] = 2 * c1 * c[i];
            }

            /* b(i1:nhlf)=(sin(c3)./c) */
            for (i = 0; i < nhlf; i++)
            {
                b[i] = (float)Math.Sin(c3[i]) / c[i];
            }

            /* bb = real([b(nhlf:-1:i1) b(1:nhlf)].*wind(:)') */
            for (i = 0, j = nhlf - 1; i < nhlf; i++, j--)
            {
                bb[i] = b[j];
            }
            for (i = nhlf-1, j = 0; i < Pr_L; i++, j++)
            {
                bb[i] = b[j];
            }
            for (i = 0; i < Pr_L; i++)
            {
                bb[i] = bb[i] * wind[i];
            }

            /* gain = abs(polyval(b,1)); */
            for (i = 0; i < Pr_L; i++)
            {
                gain += bb[i];
            }
            /* b = b / gain */
            for (i = 0; i < Pr_L; i++)
            {
                bb[i] = bb[i] / gain;
            }

            return bb;

        }



        float[] hw0 = new float[]{
            -0.00000000F,
             0.00000000F,
            -0.00000002F,
             0.00000000F,
             0.00000026F,
            -0.00000068F,
            -0.00000101F,
             0.00000724F,
            -0.00000438F,
            -0.00003711F,
             0.00006774F,
             0.00010153F,
            -0.00038510F,
            -0.00005350F,
             0.00139256F,
            -0.00083156F,
            -0.00358149F,
             0.00442054F,
             0.00672163F,
            -0.01381053F,
            -0.00878932F,
             0.03229430F,
             0.00587468F,
            -0.06172290F,
             0.00563225F,
             0.10229172F,
            -0.02471683F,
            -0.15545875F,
             0.03985025F,
             0.22829105F,
            -0.01672709F,
            -0.32678680F,
            -0.13921209F,
             0.36150230F,
             0.61049324F,
             0.47269619F,
             0.21994211F,
             0.06342378F,
             0.01054939F,
             0.00077995F
        };

        float[] gw0 = new float[] { 
            -0.00077995F,
             0.01054939F,
            -0.06342378F,
             0.21994211F,
            -0.47269619F,
             0.61049324F,
            -0.36150230F,
            -0.13921209F,
             0.32678680F,
            -0.01672709F,
            -0.22829105F,
             0.03985025F,
             0.15545875F,
            -0.02471683F,
            -0.10229172F,
             0.00563225F,
             0.06172290F,
             0.00587468F,
            -0.03229430F,
            -0.00878932F,
             0.01381053F,
             0.00672163F,
            -0.00442054F,
            -0.00358149F,
             0.00083156F,
             0.00139256F,
             0.00005350F,
            -0.00038510F,
            -0.00010153F,
             0.00006774F,
             0.00003711F,
            -0.00000438F,
            -0.00000724F,
            -0.00000101F,
             0.00000068F,
             0.00000026F,
            -0.00000000F,
            -0.00000002F,
            -0.00000000F,
            -0.00000000F
        };

        PicoRet pico(float[] FV1, int pos, int L)
        {
            // function [ik1,ik2,k1,k2]=pico(FV1,pos,L)
            int k1 = 0;
            int ik1 = pos;

            while ((FV1[ik1] > FV1[ik1 - 1]) && (ik1 > 1))
            {
                k1 = k1 + 1;
                ik1 = ik1 - 1;
            }

            ik1 = ik1 + 1;

            
           int k2 = 0;
            int ik2 = pos;
            while ((FV1[ik2] > FV1[ik2 + 1]) && (ik2 <( L - 3)))
            {
                k2 = k2 + 1;
                ik2 = ik2 + 1;
            }

            ik2 = ik2 - 1;

            return new PicoRet(ik1, ik2, k1, k2);
        }

        struct PicoRet
        {
            public int ik1, ik2, k1, k2;

            public PicoRet(int ik1, int ik2, int k1, int k2)
            {
                this.ik1 = ik1;
                this.ik2 = ik2;
                this.k1 = k1;
                this.k2 = k2;
            }
        }
    }
}
