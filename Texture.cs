using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgave_1___OpenTK
{
    internal class Texture
    {
        int handle;
        public Texture(string path)
        {
            handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, handle);

            StbImageSharp.StbImage.stbi_set_flip_vertically_on_load(1);
            //StbImageSharp.StbImage_set_flip_vertically_on_load(1);



            StbImageSharp.ImageResult image = StbImageSharp.ImageResult.FromStream(File.OpenRead(path), StbImageSharp.ColorComponents.RedGreenBlueAlpha);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
    }
}
