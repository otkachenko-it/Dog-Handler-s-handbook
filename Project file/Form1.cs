using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Справочник_кинолога
{
    public partial class Form1 : Form
    {

        ListDogs dg = new ListDogs();
        public Form1()
        {
            InitializeComponent();
            bindingSource1.DataSource = new ListDogs();

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //контур для кнопки Поиска
            System.Drawing.Drawing2D.GraphicsPath button6 = new System.Drawing.Drawing2D.GraphicsPath();
            button6.AddEllipse(0, 0, this.button6.Width, this.button6.Height);
            Region Button_Region = new Region(button6);
            this.button6.Region = Button_Region;

            //контур для кнопки сброса поиска
            System.Drawing.Drawing2D.GraphicsPath button7 = new System.Drawing.Drawing2D.GraphicsPath();
            button7.AddEllipse(0, 0, this.button7.Width, this.button7.Height);
            Region Button_Region2 = new Region(button7);
            this.button7.Region = Button_Region2;

            //контур для кнопки открытия
            System.Drawing.Drawing2D.GraphicsPath button1 = new System.Drawing.Drawing2D.GraphicsPath();
            button1.AddEllipse(0, 0, this.button1.Width, this.button1.Height);
            Region Button_Region3 = new Region(button1);
            this.button1.Region = Button_Region3;

            //контур для кнопки сохранения
            System.Drawing.Drawing2D.GraphicsPath button2 = new System.Drawing.Drawing2D.GraphicsPath();
            button2.AddEllipse(0, 0, this.button2.Width, this.button2.Height);
            Region Button_Region4 = new Region(button2);
            this.button2.Region = Button_Region4;

        }
        //Открыть
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.Xml.Serialization.XmlSerializer Olga = new System.Xml.Serialization.XmlSerializer(typeof(ListDogs));
                    System.IO.StreamReader Read = new System.IO.StreamReader(openFileDialog1.FileName);
                    bindingSource1.DataSource = (ListDogs)Olga.Deserialize(Read);
                    Read.Close();
                }

                catch (Exception)
                {
                    MessageBox.Show("Error");
                }
            }
        }
        //Сохранить
        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.Xml.Serialization.XmlSerializer Olga = new System.Xml.Serialization.XmlSerializer(typeof(ListDogs));
                    System.IO.StreamWriter Reader = new System.IO.StreamWriter(saveFileDialog1.FileName);
                    Olga.Serialize(Reader, bindingSource1.DataSource);
                    Reader.Close();
                }

                catch (Exception)
                {
                    MessageBox.Show("Error");
                }
            }
        }
        //Фильтрация
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ListDogs dog = new ListDogs();
                dg = (ListDogs)bindingSource1.DataSource;

                switch (comboBox1.Text)
                {
                    case "Breed": dog.Dogs = (from x in dg.Dogs where x.Breed == textBox1.Text select x).ToList<Kinolog>(); break;
                    case "Weight": dog.Dogs = (from x in dg.Dogs where x.Weight.ToString() == textBox1.Text select x).ToList<Kinolog>(); break;
                    case "Height": dog.Dogs = (from x in dg.Dogs where x.Height.ToString() == textBox1.Text select x).ToList<Kinolog>(); break;
                    case "Color": dog.Dogs = (from x in dg.Dogs where x.Color == textBox1.Text select x).ToList<Kinolog>(); break;
                    case "Age": dog.Dogs = (from x in dg.Dogs where x.Age.ToString() == textBox1.Text select x).ToList<Kinolog>(); break;
                }

                bindingSource1.DataSource = dog;
                bindingSource1.MoveFirst();
                dataGridView1.Refresh();
            }

            catch
            {
                MessageBox.Show("Фильтрация не удалась!");
            }
        }
        //Сортировка
        private void button3_Click(object sender, EventArgs e)
        {
            ListDogs ddog = (ListDogs)bindingSource1.DataSource;
            int t = 1;
            if (radioButton1.Checked == true)
                t = -1;

            switch (comboBox1.Text)
            {
                case "Breed": ddog.Dogs.Sort((x, y) => t * (x.Breed.CompareTo(y.Breed))); break;
                case "Weight": ddog.Dogs.Sort((x, y) => t * (x.Weight.CompareTo(y.Weight))); break;
                case "Height": ddog.Dogs.Sort((x, y) => t * (x.Height.CompareTo(y.Height))); break;
                case "Color": ddog.Dogs.Sort((x, y) => t * (x.Color.CompareTo(y.Color))); break;
                case "Age": ddog.Dogs.Sort((x, y) => t * (x.Age.CompareTo(y.Age))); break;
            }

            bindingSource1.DataSource = ddog;
            bindingSource1.MoveFirst();
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.Refresh();
        }
        //Сброс фильтра
        private void button5_Click(object sender, EventArgs e)
        {
            bindingSource1.DataSource = dg;
            bindingSource1.MoveFirst();
            dataGridView1.Refresh();
        }

        private void breedtextBox1_TextChanged(object sender, EventArgs e)
        {
        }


        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
        //Поиск
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {
                    dataGridView1.CurrentCell = null;
                    dataGridView1.Rows[i].Visible = false;


                    for (int c = 0; c < dataGridView1.Columns.Count; c++)
                    {
                        if (dataGridView1[c, i].Value.ToString().Equals(textBox2.Text))
                        {
                            dataGridView1.Rows[i].Visible = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception) { }
        }
        //Сброс поиска
        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {  
                dataGridView1.Rows[i].Visible = true;
            }
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                textBox1.Text = "Enter";
                textBox1.ForeColor = Color.LightSlateGray;
            }
        }

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Enter")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Enter";
                textBox2.ForeColor = Color.LightSlateGray;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        //Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_MouseEnter(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Choose")
            {
                comboBox1.Text = "";
                comboBox1.ForeColor = Color.Black;
            }
        }

        private void comboBox1_MouseLeave(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                comboBox1.Text = "Choose";
                comboBox1.ForeColor = Color.LightSlateGray;
            }
        }
    }

    //Класс с переменными
    public class Kinolog : IComparable<Kinolog>
    {
        public string Breed { set; get; }
        public double Weight { set; get; }
        public double Height { set; get; }
        public string Color { set; get; }
        public double Age { set; get; }

        public Kinolog()
        {
            Breed = "";
            Weight = 0;
            Height = 0;
            Color = "";
            Age = 0;
        }

        public override bool Equals(object obj)
        {
            return this.Age == ((Kinolog)obj).Age;
        }

        public int CompareTo(Kinolog k)
        {
            return this.Age.CompareTo(k.Age);
        }
    }
    //Класс списка
    public class ListDogs
    {
        public List<Kinolog> Dogs { set; get; }
        public ListDogs() { Dogs = new List<Kinolog>(); }
    }

}
