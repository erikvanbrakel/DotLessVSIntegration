using System;

namespace LessProject.DotLessIntegration.LexerSrc
{
    public class StringCharacterBuffer : ICharacterBuffer
    {
        private int currentPosition;
        private readonly int[] bufferArray;
        private readonly string code;
        private readonly int size;

        private int actualPosition = 0 ;


        public StringCharacterBuffer(string script, int bufferSize)
        {
            //Initialize bufferArrayfer
            code = script;
            size = bufferSize;
            bufferArray = new int[size];
            SetPos(0);
        }

        public void SetPos(int position)
        {
            currentPosition = position;
            // bufferArray[size - 1] = code[currentPosition++];
        }
        /// <summary>
        /// Load next set of charactors into the buffer
        /// </summary>
        public void Load(int length)
        {
            if(length > size) length = size;
            for (var i = 1; i <= length; i++)
                Load();
        }

        /// <summary>
        /// Load next charactor into the buffer
        /// </summary>
        public void Load()
        {
            for (var i = 0; i < size - 1; i++){
                bufferArray[i] = bufferArray[i + 1];
            }
            try{
                if (currentPosition == code.Length){
                    bufferArray[size - 1] = -1;
                    actualPosition = currentPosition;
                }
                else{
                    bufferArray[size - 1] = code[currentPosition++];
                    actualPosition = currentPosition - size;
                }
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Peeks at the charactor(s) from the buffer
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int Peek(int pos)
        {
            if (pos >= 1 && pos <= size)
                return bufferArray[pos - 1];
            return 0;
        }

        public int CurrentPos
        {
            get { return actualPosition; }
        }
    }
}