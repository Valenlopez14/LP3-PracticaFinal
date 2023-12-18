using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;


namespace DienteFeliz
{
    internal class clsOdontologo
    {
        private OleDbConnection conector;
        private OleDbCommand comando;
        private OleDbDataAdapter adaptador;
        private DataTable tabla;
        DataSet objds = new DataSet();

        public clsOdontologo()
        {
            conector = new OleDbConnection(Properties.Settings.Default.CADENA);
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Odontologos";

            adaptador = new OleDbDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);

            DataColumn[] dc = new DataColumn[1];
            dc[0] = tabla.Columns["matricula"];
            tabla.PrimaryKey = dc;

        }

        public DataTable GetDataOdontologos()
        {
            return tabla;
        }

        public string ObtenerMatricula(string nombreOdontologo)
        {
            string matricula = "";
            foreach (DataRow fOdontologo in tabla.Rows)
            {
                if (fOdontologo["nombre"].ToString() == nombreOdontologo)
                {
                    matricula = fOdontologo["matricula"].ToString();
                }
            }

            return matricula;

        }

        public void CargarOdontologos(ListView lista)
        {
            foreach (DataRow fila in tabla.Rows)
            {
                ListViewItem item = lista.Items.Add(fila["nombre"].ToString());
                    item.Tag = fila["matricula"].ToString();
            }
        }
    }
}
