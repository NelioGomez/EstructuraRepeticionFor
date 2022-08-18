namespace pjSeguroVida
{
    public partial class frmProforma : Form
    {
        public frmProforma()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            //Capturando los datos
            string razon = txtRazonSocial.Text;
            string tipo = cboTipo.Text;
            int cantidad = int.Parse(txtCantidad.Text);

            //calculando el pago mensual por tipo de seguro
            double pagomensual = 0;

            switch (tipo)
            {
                //primer case
                case "Inversion Clasica":
                    if (cantidad <= 10)
                        pagomensual = (50 * cantidad);
                    else
                        pagomensual = (50 * cantidad) + (10 * (cantidad - 10));
                    break;
                
                //Segundo case
                case "Inversion Platino":
                    if (cantidad <= 9)
                        pagomensual = (80 * cantidad);
                    else
                        pagomensual = (80 * cantidad) + (9 * (cantidad - 9));
                    break;

                //Tercer case
                case "Inversion Oro":
                    if (cantidad <= 6)
                        pagomensual = (150 * cantidad);
                    else
                        pagomensual = (150 * cantidad) + (6 * (cantidad - 6));
                    break;
            }

            //imprimiendo el detalle de la proforma
            ListViewItem fila = new ListViewItem(tipo);
            fila.SubItems.Add(cantidad.ToString());
            fila.SubItems.Add(pagomensual.ToString("0.00"));
            lvProforma.Items.Add(fila);
        }

        private void btnGeneral_Click(object sender, EventArgs e)
        {
            //Determinar el total de personas aseguradas
            int totalAsegurados = 0;

            for (int i = 0; i < lvProforma.Items.Count; i++)
            {
                //se agreaga if por si acaso no tiene caracteres en la columna 0
                if (lvProforma.Items[i].SubItems[0].Text != "")
                    totalAsegurados += int.Parse(lvProforma.Items[i].SubItems[1].Text);
            }

            //Determinar el monto total acumulado a cancelar
            double total = 0;
            for (int i = 0; i < lvProforma.Items.Count; i++)
            {
                //se agreaga if por si acaso no tiene caracteres en la columna 0
                if (lvProforma.Items[i].SubItems[0].Text != "")
                    total += double.Parse(lvProforma.Items[i].SubItems[2].Text);
            }

            // Impresion de las estadisticas
            lvEstadistica.Items.Clear();
            string[] elementosFila = new string[2];
            ListViewItem row;

            //primera fila
            elementosFila[0] = "Total de personas aseguradas";
            elementosFila[1] = totalAsegurados.ToString();
            row = new ListViewItem(elementosFila);
            lvEstadistica.Items.Add(row);

            //segunda fila
            elementosFila[0] = "Monto total a cancelar";
            elementosFila[1] = total.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadistica.Items.Add(row);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("¿Esta seguro que desea salir?",
                                          "Control de seguro de vida",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
                this.Close();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("¿Esta seguro de anular la proforma?",
                                         "Control de seguro de vida",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                txtRazonSocial.Clear();
                cboTipo.Text = "(Seleccione tipo)";
                txtCantidad.Clear();
                txtRazonSocial.Focus();
                lvProforma.Items.Clear();
                lvEstadistica.Items.Clear();
            }
        }

    }
}