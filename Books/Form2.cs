using System;
using System.Drawing;
using System.Windows.Forms;

namespace Books
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //int image_width = Convert.ToInt32(textBox1.Text);
            //int image_height = Convert.ToInt32(textBox2.Text);


            int dest_width = Convert.ToInt32(textBox3.Text);
            int dest_height = Convert.ToInt32(textBox4.Text);

            Image bm = Image.FromFile(@"C:\Users\q8382\Desktop\3.png");
            pictureBox1.Size = new Size(dest_width, dest_height);


            Bitmap bm1 = CropCenterRect(bm, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bm1;

        }

        public Bitmap CreateBitmap(int width, int height)
        {
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            g.FillRectangle(Brushes.Green, 0, 0, width, height);

            return bm;
        }




        /// <summary>
        /// 居中缩放图像
        /// </summary>
        /// <param name="src">源</param>
        /// <param name="dest">目标</param>
        public static RectangleF ImageScale(Image src, float width, float height)
        {
            double srcScale;
            double destScale;

            srcScale = (double)src.Width / src.Height;
            destScale = (double)width / height;


            //计算长宽比
            if (srcScale - destScale >= 0 && srcScale - destScale <= 0.001)
            {
                //长宽比相同
                return new RectangleF(0, 0, width, width);
            }
            else if (srcScale < destScale)
            {
                //源长宽比小于目标长宽比，源的高度大于目标的高度
                float newWidth = (float)width * height / src.Height;
                return new RectangleF((width - newWidth) / 2, 0, newWidth, height);
            }
            else
            {
                //源长宽比大于目标长宽比，源的宽度大于目标的宽度
                float newHeight = (float)height * src.Width / width;
                return new RectangleF(0, (height - newHeight) / 2, width, newHeight);
            }
        }


        // 居中缩放
        private Bitmap CropCenterRect(Image image, float width, float height)
        {
            //得到图片斜率
            float image_scale = (float)image.Width / image.Height;
            //得到容器斜率
            float dest_scale = width / height;

            float new_width = 0, new_height = 0;


            if (Math.Abs(image_scale - dest_scale) < 0.01)
            {
                // 斜率相同，直接缩放就行
                new_width = width;
                new_height = height;
            }
            else
            {
                new_width = height / image.Height * image.Width;
                if (new_width <= width)
                {
                    new_height = height;
                }
                else
                {
                    new_height = width / image.Width * image.Height;
                    new_width = width;
                }
            }

            RectangleF destRect = new RectangleF((width - new_width) / 2, (height - new_height) / 2, new_width, new_height);
            Bitmap bm = new Bitmap((int)width, (int)height);
            Graphics g = Graphics.FromImage(bm);

            g.DrawImage(image, destRect, new RectangleF(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            return bm;
        }
    }
}
