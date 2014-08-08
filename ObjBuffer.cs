using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GuiSonar2
{
    public class ObjBuffer
    {
        private int bufferLen;
        private float[] bufferData;
        private int wrPos;

        public ObjBuffer(double bufferSize)
        {
            if (bufferSize < 1)
            {
                Console.WriteLine("bufferSize es menor a 1!");
                bufferSize = 1;
            }

            bufferLen = (int)Math.Round(bufferSize);
            wrPos = 0;

            bufferData = new float[bufferLen];
        }
        Stopwatch stopwatch = new Stopwatch();

        public void push(float[] newData)
        {
            int newDataLen = newData.Length;
            int wPos = wrPos;
            int bLen = bufferLen;
            int[] idxNewData = new int[newDataLen];
            for (int i = wPos; i <= (wPos + newDataLen - 1); i++)
            {
                idxNewData[i - wPos] = i % bLen;
                bufferData[idxNewData[i - wPos]] = newData[i - wPos];
            }

            wrPos = wPos + newDataLen;
            if (wrPos >= bLen)
            {
                wrPos = wrPos % bLen;
                stopwatch.Stop();
                Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
                stopwatch.Reset();
                stopwatch.Start();
            }
            // Console.WriteLine(wrPos); 

        }


        public void clearBuff()
        {
            Array.Clear(bufferData, 0, bufferData.Length);
            Console.WriteLine("error ObjBuffer-->clearBuff");
        }

        public void setBuffLength(int newBufferSize)
        {
            int newBufferLen = newBufferSize;
            int oldBufferLen = bufferLen;
            int wPos = 0;
            int bLen = 0;
            int[] idxOldData;
            float[] oldData;

            if (newBufferLen != bufferLen)
            {
                wPos = wrPos;
                bLen = bufferData.Length;
                idxOldData = new int[bLen];
                oldData = new float[bLen];

                for (int i = wPos; i <= wPos + bLen - 1; i++)
                {
                    idxOldData[i - wPos] = i % bLen;
                }

                for (int i = 0; i < bLen; i++)
                {
                    oldData[i] = bufferData[idxOldData[i]];
                }

                int lendDiff = oldBufferLen - newBufferLen;

                if (lendDiff < 0)
                {
                    for (int i = 0; i < (-lendDiff); i++)
                    {
                        bufferData[i] = 0;
                    }
                    for (int i = 0; i < bLen; i++)
                    {
                        bufferData[i + lendDiff] = oldData[i];
                    }
                }
                else
                {
                    for (int i = (lendDiff + 1); i < bLen; i++)
                    {
                        bufferData[i - (lendDiff + 1)] = oldData[i];
                    }

                }

                bufferLen = newBufferLen;
                wrPos = 0;

            }
        }

        float[] getBuffer()
        {
            float[] buffer;

            buffer = new float[bufferData.Length];
            buffer = bufferData;

            return buffer;
        }

        float[] getFifoBuffer()
        {
            int wPos = wrPos;
            int bLen = bufferLen;
            int[] idxBuffData = new int[bLen];
            float[] ordBuffer = new float[bLen];
            for (int i = wPos + 1; i <= wPos + bLen - 1; i++)
            {
                idxBuffData[i] = i % bLen;
            }

            for (int i = 0; i < bLen; i++)
            {
                ordBuffer[i] = bufferData[idxBuffData[i]];
            }

            return ordBuffer;
        }
    }

    public class ObjBuffer2
    {
        private int bufferLen;
        private float[][] bufferData;
        private int wrPos;

        public ObjBuffer2()//int bufferSize
        {
            /*if (bufferSize < 1)
               {
                   Console.WriteLine("bufferSize es menor a 1");
                   bufferSize = 1;
               }*/
            wrPos = 0;
            bufferLen = 3; // bufferSize;
            bufferData = new float[bufferLen][];
            bufferData[0] = new float[256];
            bufferData[1] = new float[256];
            bufferData[2] = new float[256];
        }

        public void push(float[] newData)
        {
            if (bufferData[0].Length > 0)
            {
                if (newData.Length != bufferData[0].Length)
                {
                    Console.WriteLine("wrong size of new data");
                    return;
                }
            }

            int wPos = wrPos;
            int bLen = bufferLen;
            int[] idxNewData = new int[bLen];
            //for (int i = wPos; i <(wPos + 1);i++)
            //{
            if (wPos == 3)
            {
                wPos = 0;
            }
            idxNewData[wPos] = wPos;//%bLen;
            //}

            //for (int i = 0; i < 1; i++)
            //{
            bufferData[idxNewData[0]] = newData;
            //}
            wrPos = wPos + 1;
        }


        public void clearBuff()// al parecer no se usa
        {
            Array.Clear(bufferData, 0, 3);
            Console.WriteLine("error clearbuff objbuff2");
        }

        public float[][] getBuffer()
        {
            float[][] buffer;

            buffer = new float[bufferData.Length][];
            buffer = bufferData;

            return buffer;
        }

        public float[] getMean()
        {
            float[] avg = new float[bufferData[0].Length];
            float[][] buffer = new float[bufferLen][];
            buffer = bufferData;

            for (int i = 0; i < bufferData[0].Length; i++)
            {
                avg[i] = buffer[0][i] + buffer[1][i] + buffer[2][i];
            }

            return avg;

        }
    }

}
