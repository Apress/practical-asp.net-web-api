using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Tracing;

namespace HelloWebApi
{
    public class RingBufferLog
    {
        private const int BUFFER_SIZE = 1000;

        TraceRecord[] buffer;
        int pointer = 0;
        private readonly object myPrecious = new object();

        private static RingBufferLog instance = new RingBufferLog();

        private RingBufferLog()
        {
            buffer = new TraceRecord[BUFFER_SIZE];
            ResetPointer();
        }

        public IList<TraceRecord> DequeueAll()
        {
            lock (myPrecious)
            {
                ResetPointer();

                var bufferCopy = new List<TraceRecord>(buffer.Where(t => t != null));

                for (int index = 0; index < BUFFER_SIZE; index++)
                {
                    buffer[index] = null;
                }

                return bufferCopy;
            }
        }

        public IList<TraceRecord> PeekAll()
        {
            lock (myPrecious)
            {
                var bufferCopy = new List<TraceRecord>(buffer.Where(t => t != null));

                return bufferCopy;
            }
        }

        private void ResetPointer()
        {
            pointer = BUFFER_SIZE - 1;
        }

        private void MovePointer()
        {
            pointer = (pointer + 1) % BUFFER_SIZE;
        }

        public void Enqueue(TraceRecord item)
        {
            lock (myPrecious)
            {
                MovePointer();
                buffer[pointer] = item;
            }
        }

        public static RingBufferLog Instance
        {
            get
            {
                return instance;
            }
        }
    }

}