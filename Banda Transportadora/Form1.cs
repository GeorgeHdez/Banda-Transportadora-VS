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

namespace Banda_Transportadora
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            Get_Port_Names();
            button2.Enabled = false;
            
            
        }
        void Get_Port_Names()
        {
            string[] names = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(names);
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
    }
}
