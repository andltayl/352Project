using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _352Project
{
    public class PinkLlama : LlamaDecorator, llamaSkin
    {
        public PinkLlama(llamaSkin newSkin) : base(newSkin)
        {
            
        }

        public new string getSkin()
        {
            return "pack://application:,,,/Resources/llama22.png";
        }
    }
}
