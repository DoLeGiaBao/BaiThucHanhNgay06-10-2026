using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bai5_4
{
    public partial class Form1 : Form
    {
        private List<Employee> employees = new List<Employee>();

        public Form1()
        {
            InitializeComponent();

            // Gán ImageList
            tvDepartments.ImageList = imageList1;
            lsvEmployees.SmallImageList = imageList1;
            lsvEmployees.LargeImageList = imageList1;

            // Tạo dữ liệu mẫu
            CreateSampleData();

            // Tạo cây TreeView
            CreateTreeView();

            // Thêm các chế độ hiển thị
            cboViewMode.Items.Add("Details");
            cboViewMode.Items.Add("SmallIcon");
            cboViewMode.Items.Add("LargeIcon");
            cboViewMode.Items.Add("Tile");

            cboViewMode.SelectedIndex = 0;

            // Hiển thị toàn bộ nhân viên
            LoadEmployees(employees);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void CreateSampleData()
        {
            employees.Add(new Employee(
                "NV001",
                "Nguyễn Văn An",
                "Developer",
                new DateTime(2022, 3, 15),
                "Phòng Kỹ thuật",
                "Developer"));

            employees.Add(new Employee(
                "NV002",
                "Trần Thị Bình",
                "Tester",
                new DateTime(2021, 7, 20),
                "Phòng Kỹ thuật",
                "Tester"));

            employees.Add(new Employee(
                "NV003",
                "Lê Văn Cường",
                "Developer",
                new DateTime(2023, 1, 10),
                "Phòng Kỹ thuật",
                "Developer"));

            employees.Add(new Employee(
                "NV004",
                "Phạm Thị Dung",
                "Nhân viên Sales",
                new DateTime(2020, 5, 12),
                "Phòng Kinh doanh",
                "Sales"));

            employees.Add(new Employee(
                "NV005",
                "Hoàng Văn Em",
                "Nhân viên Marketing",
                new DateTime(2022, 9, 1),
                "Phòng Kinh doanh",
                "Marketing"));

            employees.Add(new Employee(
                "NV006",
                "Vũ Thị Hoa",
                "Nhân viên Sales",
                new DateTime(2021, 11, 5),
                "Phòng Kinh doanh",
                "Sales"));

            employees.Add(new Employee(
                "NV007",
                "Đỗ Văn Khánh",
                "HR Manager",
                new DateTime(2019, 4, 20),
                "Phòng Nhân sự",
                "Nhân sự"));

            employees.Add(new Employee(
                "NV008",
                "Nguyễn Thị Lan",
                "HR Staff",
                new DateTime(2023, 6, 15),
                "Phòng Nhân sự",
                "Nhân sự"));
        }

        private void CreateTreeView()
        {
            tvDepartments.Nodes.Clear();

            TreeNode companyNode =
                new TreeNode("Công ty AutoSpeed");

            TreeNode kinhDoanhNode =
                new TreeNode("Phòng Kinh doanh");

            TreeNode salesNode =
                new TreeNode("Nhóm Sales");

            salesNode.Tag = "Phòng Kinh doanh|Sales";

            TreeNode marketingNode =
                new TreeNode("Nhóm Marketing");

            marketingNode.Tag = "Phòng Kinh doanh|Marketing";

            kinhDoanhNode.Nodes.Add(salesNode);
            kinhDoanhNode.Nodes.Add(marketingNode);

            TreeNode kyThuatNode =
                new TreeNode("Phòng Kỹ thuật");

            TreeNode developerNode =
                new TreeNode("Nhóm Developer");

            developerNode.Tag = "Phòng Kỹ thuật|Developer";

            TreeNode testerNode =
                new TreeNode("Nhóm Tester");

            testerNode.Tag = "Phòng Kỹ thuật|Tester";

            kyThuatNode.Nodes.Add(developerNode);
            kyThuatNode.Nodes.Add(testerNode);

            TreeNode nhanSuNode =
                new TreeNode("Phòng Nhân sự");

            TreeNode nhanSuGroupNode =
                new TreeNode("Nhóm Nhân sự");

            nhanSuGroupNode.Tag = "Phòng Nhân sự|Nhân sự";

            nhanSuNode.Nodes.Add(nhanSuGroupNode);

            companyNode.Nodes.Add(kinhDoanhNode);
            companyNode.Nodes.Add(kyThuatNode);
            companyNode.Nodes.Add(nhanSuNode);

            tvDepartments.Nodes.Add(companyNode);

            companyNode.Expand();
            kinhDoanhNode.Expand();
            kyThuatNode.Expand();
            nhanSuNode.Expand();
        }

        private void LoadEmployees(List<Employee> employeeList)
        {
            lsvEmployees.Items.Clear();

            foreach (Employee employee in employeeList)
            {
                ListViewItem item =
                    new ListViewItem(employee.MaNV);

                item.SubItems.Add(employee.HoTen);
                item.SubItems.Add(employee.ChucVu);
                item.SubItems.Add(
                    employee.NgayVaoLam.ToString("dd/MM/yyyy"));

                item.ImageIndex = 0;

                lsvEmployees.Items.Add(item);
            }
        }

        private void tvDepartments_AfterSelect(
            object sender,
            TreeViewEventArgs e)
        {
            if (e.Node.Tag == null)
            {
                LoadEmployees(employees);
                return;
            }

            string tag = e.Node.Tag.ToString();

            string[] parts = tag.Split('|');

            if (parts.Length != 2)
                return;

            string phongBan = parts[0];
            string nhom = parts[1];

            List<Employee> filteredEmployees =
                employees.FindAll(employee =>
                    employee.PhongBan == phongBan &&
                    employee.Nhom == nhom);

            LoadEmployees(filteredEmployees);
        }

        private void cboViewMode_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            switch (cboViewMode.SelectedIndex)
            {
                case 0:
                    lsvEmployees.View = View.Details;
                    break;

                case 1:
                    lsvEmployees.View = View.SmallIcon;
                    break;

                case 2:
                    lsvEmployees.View = View.LargeIcon;
                    break;

                case 3:
                    lsvEmployees.View = View.Tile;
                    break;
            }
        }
    }
}