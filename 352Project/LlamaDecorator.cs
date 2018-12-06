using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _352Project
{
    public abstract class LlamaDecorator : llamaSkin
    {
        private llamaSkin tempLlama;

        public LlamaDecorator(llamaSkin newSkin)
        {
            tempLlama = newSkin;
        }

        public string getSkin()
        {
            return tempLlama.getSkin();
        }
    }
}
