using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace DienteFeliz
{
    public partial class Form1 : Form
    {

        clsHorario objHorario;
        clsOdontologo clsOdontologo;
        Turnos objTurnos;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                objHorario = new clsHorario();
                clsOdontologo = new clsOdontologo();
                objTurnos = new Turnos(); 


            }
            catch (Exception)
            {

                MessageBox.Show("Error con las Tablas");
            }

            DataTable TablaOdontologos = new DataTable();
            DataTable TablaTurnos = new DataTable();
            DataTable TablaHorarios = new DataTable();

            TablaOdontologos = clsOdontologo.GetDataOdontologos();
            TablaTurnos = objTurnos.GetDataTurnos();
            TablaHorarios = objHorario.GetDataHorarios();

            TreeNode abuelo;
            TreeNode padre;
            TreeNode hijo;

            abuelo = treeView1.Nodes.Add("Odontologos");
            foreach (DataRow fOdontologos in TablaOdontologos.Rows)
            {
                padre = abuelo.Nodes.Add(fOdontologos["nombre"].ToString());
                padre.Tag = fOdontologos["matricula"].ToString();

                foreach (DataRow fPacientes in TablaTurnos.Rows)
                {
                    if (padre.Tag.ToString() == fPacientes["matricula"].ToString())
                    {
                        hijo = padre.Nodes.Add(fPacientes["paciente"].ToString());
                    }
                }
            }

            clsOdontologo.CargarOdontologos(listView1);


        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DataTable TablaOdontologos = new DataTable();
            DataTable TablaTurnos = new DataTable();
            DataTable TablaHorarios = new DataTable();

            TablaOdontologos = clsOdontologo.GetDataOdontologos();
            TablaTurnos = objTurnos.GetDataTurnos();
            TablaHorarios = objHorario.GetDataHorarios();

            string OdontologoSeleccionado = e.Node.Text.ToString();
            string matricula = clsOdontologo.ObtenerMatricula(OdontologoSeleccionado.ToString());

            dataGridView1.Rows.Clear();

            if (e.Node.Level == 1)
            {
                foreach (DataRow fTurnos in TablaTurnos.Rows)
                {
                    if (fTurnos["matricula"].ToString() == matricula)
                    {
                        dataGridView1.Rows.Add(fTurnos["paciente"], fTurnos["fecha"], fTurnos["hora"]);
                    }
                }

            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable TablaOdontologos = new DataTable();
            DataTable TablaTurnos = new DataTable();
            DataTable TablaHorarios = new DataTable();

            TablaOdontologos = clsOdontologo.GetDataOdontologos();
            TablaTurnos = objTurnos.GetDataTurnos();
            TablaHorarios = objHorario.GetDataHorarios();

            Series serie = new Series();
            chart1.Series.Clear();
           


            foreach (ListViewItem item in listView1.Items)
            {
                string nombreOdontologo = item.Text;
                int total = 0;  // Inicializa total para cada odontólogo

                if (item.Checked)
                {
                    foreach (DataRow fTurnos in TablaTurnos.Rows)
                    {
                        
                        if (fTurnos["matricula"].ToString() == item.Text.ToString())
                        {
                            
                            total = total + 1;
                        }

                    }

                    
                    chart1.Series.Add(nombreOdontologo);
                    chart1.Series[nombreOdontologo].Points.AddXY(nombreOdontologo, total);
                    total = 0;
                }
            }


        }
    }
}
