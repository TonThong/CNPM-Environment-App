using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Environmental_Monitoring.Controller.Data;
using Environmental_Monitoring.Model;

namespace Environmental_Monitoring.View.Contract_pages
{
    public partial class ContractResult : Contract
    {

        private ContractRepo _repo;
        private BindingList<Model.Contract> _binding;

        public ContractResult()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void ContractResult_Load(object sender, EventArgs e)
        {
            string connStr = "Server=sql12.freesqldatabase.com;Database=sql12800882;User ID=sql12800882;Password=TMlsWFrPxZ;Port=3306;";
            _repo = new ContractRepo(connStr);
            var list = _repo.GetAll();
            _binding = new BindingList<Model.Contract>(list);


        }
    }
}
