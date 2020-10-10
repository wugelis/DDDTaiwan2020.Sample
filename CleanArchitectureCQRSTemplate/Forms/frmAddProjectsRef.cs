using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanArchitectureCQRSTemplate.Forms
{
    public partial class frmAddProjectsRef : Form
    {
        private IEnumerable<string> _projectNams = null;
        private static CheckedListBox _selectedCheckListBoxItem;

        public frmAddProjectsRef()
        {
            InitializeComponent();
        }

        public frmAddProjectsRef(IEnumerable<string> projectNames)
        {
            InitializeComponent();

            _projectNams = projectNames;

            chkListBxVsProjects.Items.AddRange(_projectNams.ToArray());

            frmAddProjectsRef._selectedCheckListBoxItem = chkListBxVsProjects;
        }

        public static CheckedListBox GetSelectCheckListBox
        {
            get => _selectedCheckListBoxItem;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
