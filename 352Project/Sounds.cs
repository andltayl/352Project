using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _352Project
{
    class Sounds
    {
        MediaPlayer mplayer = new MediaPlayer();

        public void PlaySound()
        {

            mplayer.Open(new Uri("pack://application:,,,/_352Project;component/Resource/Sounds/Effects/menuMusic.mp3", UriKind.Absolute));

            mplayer.Play();
        }
    }
}
