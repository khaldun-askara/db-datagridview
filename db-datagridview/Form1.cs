using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_datagridview
{
    public partial class client_coach : Form
    {
        public client_coach()
        {
            InitializeComponent();
            database_funcs.InitializeDGVClients(dgv_clients);
            database_funcs.InitializeDGVCoaches(dgv_coaches);
        }
    }
}
