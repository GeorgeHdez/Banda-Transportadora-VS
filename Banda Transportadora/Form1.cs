using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Windows;

 
namespace Banda_Transportadora
{
    
    public partial class Form1 : Form
    {

        bool flag_IO = false;
        bool flag_stop = false;
        bool robot_enable = false; // led que indica cuando colocar la tapa
        bool led_position = false; // led que indica cuando la tapa se posiciono
        int tapas_derecha = 0;
        int tapas_izquierda = 0;
        byte[] recive_buffer = new byte[4];

        

        Thread Data_Transmition; // Declaracion del hilo para recivir datos
        Thread Data_Work;

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            Get_Port_Names(); // Funcion para obtener todos los nombres de los puertos COM disponibles
            button2.Enabled = false;
            pictureBox2.Visible = false;
            pictureBox1.Visible = false;
            Control.CheckForIllegalCrossThreadCalls = false;


        }

        void Get_Port_Names()
        {
            string[] names = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(names);
        }
        void data_w()
        {
            
            tapas_derecha = recive_buffer[2];
            tapas_izquierda = recive_buffer[3];

            if (recive_buffer[0] == 1) robot_enable = true;
            else if (recive_buffer[0] == 0) robot_enable = false;
            else MessageBox.Show("Dato de robot disponible no correcto");

            if (recive_buffer[1] == 1) led_position = true;
            else if (recive_buffer[1] == 0) led_position = false;
            else MessageBox.Show("Datos de posicion no correcto");

            label5.Text = Convert.ToString(tapas_derecha);
            label6.Text = Convert.ToString(tapas_izquierda);

            if (robot_enable == true) pictureBox1.Visible = true;
            else pictureBox1.Visible = false;

            if (led_position == true) pictureBox2.Visible = true;
            else pictureBox2.Visible = false;

        }
        void data_rs() // HILO PARA RECIBIR DATOS EN UN DETERMINADO TIEMPO
        {
            byte[] num = new byte[1];
            num[0] = Convert.ToByte('s');
            while (flag_stop)
            {

                serialPort1.Write(num, 0, 1);
                for (int i = 0; i < 4; i++)
                {
                recive_buffer[i] = (byte)serialPort1.ReadChar();



                }
                for (int i = 0; i < 4; i++)
                {
                    richTextBox1.Text += Convert.ToString(recive_buffer[i]);
                }
                richTextBox1.Text += "\t";
                data_w();
                Thread.Sleep(3000);
            }
        }
    

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.BaudRate = Convert.ToInt16(comboBox2.Text);
            serialPort1.DataBits = 8;
            serialPort1.Parity = 0;
            serialPort1.PortName = comboBox1.Text;

            try
            {
                serialPort1.Open();
                MessageBox.Show("Conexion Serial Establecida");
                button1.Enabled = false;
                button2.Enabled = true;
            }
            catch
            {
                MessageBox.Show("ERROR ---- La conexion no se pudo establecer...");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                button1.Enabled = true;
                button2.Enabled = false;
                MessageBox.Show("Conexion Serial Desconectada");
            }
        }

        public void button3_Click(object sender, EventArgs e)
        {
            byte[] send = new byte[4];
            send[0] = Convert.ToByte('s');
            flag_stop = true;
            Data_Transmition = new Thread(new ThreadStart(data_rs));
            //Data_Work = new Thread(new ThreadStart(data_w));
            Data_Transmition.Start();
            //Data_Work.Start();
            
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            //flag_stop = false;
            Data_Transmition.Abort();
            //Data_Work.Abort();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
