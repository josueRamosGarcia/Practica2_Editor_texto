namespace Practica2_Editor_texto
{
    public partial class frmEditor : Form
    {

        bool archivoGuardado = false;
        bool textoModificado = false;
        bool archivoAbierto = false;
        string filePath = null;

        public frmEditor()
        {
            InitializeComponent();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textoModificado == true)
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Desea guardar los cambios realizados", "Sistema", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                    guardarToolStripMenuItem_Click(this, EventArgs.Empty);
            }
            archivoGuardado = false;
            rtbEditor.Clear();
            MessageBox.Show("Nuevo archivo");
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            resultado = openFileDialogEditor.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                filePath = openFileDialogEditor.FileName;
                try
                {
                    archivoAbierto = true;
                    textoModificado = false;
                    string texto = File.ReadAllText(filePath);
                    rtbEditor.Text = texto;
                    archivoGuardado = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al abrir el archivo: " + ex.Message);
                }
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (archivoGuardado == false)
                guardarComoToolStripMenuItem_Click(this, EventArgs.Empty);
            else
            {
                try
                {
                    string texto = rtbEditor.Text;
                    File.WriteAllText(filePath, texto);
                    MessageBox.Show("Archivo guardado correctamente");
                    textoModificado = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el archivo: " + ex.Message);
                }
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            resultado = saveFileDialogEditor.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                filePath = saveFileDialogEditor.FileName;
                string texto = rtbEditor.Text;
                try
                {
                    File.WriteAllText(filePath, texto);
                    MessageBox.Show("Archivo guardado correctamente");
                    archivoGuardado = true;
                    textoModificado = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el archivo: " + ex.Message);
                }
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textoModificado == false)
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Desea salir", "Sistema", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                    this.Close();
            }
            else
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Desea guardar los cambios realizados", "Sistema", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                    guardarToolStripMenuItem_Click(this, EventArgs.Empty);
                this.Close();
            }
        }

        private void rtbEditor_TextChanged(object sender, EventArgs e)
        {
            if (archivoAbierto == false)
                textoModificado = true;

            if (archivoAbierto == true)
                archivoAbierto = false;
        }

    }
}
