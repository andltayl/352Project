using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _352Project
{
    public class BrownLlama : LlamaDecorator, llamaSkin
    {
        public BrownLlama(llamaSkin newSkin) : base(newSkin)
        {

        }

        public new string getSkin()
        {
            return "pack://application:,,,/Resources/llama2.png";
        }
    }
} 
