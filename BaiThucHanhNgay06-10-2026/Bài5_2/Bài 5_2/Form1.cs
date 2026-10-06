using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Bài_5_2
{
    public partial class Form1 : Form
    {
        public class Service
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public override string ToString() => $"{Name} - {Price:N0} VNĐ";
        }

        public class CategoryItem
        {
            public string CategoryName { get; set; }
            public List<Service> Services { get; set; }
            public override string ToString() => CategoryName;
        }

        private ComboBox cboCategory;
        private ListBox lstAvailableServices;
        private ListBox lstSelectedServices;
        private Button btnSelect;
        private Button btnRemove;
        private Button btnClearAll;
        private Label lblTotal;
        private List<CategoryItem> categories;

        public Form1()
        {
            InitializeComponentCustom();
            InitData();
        }

        private void InitializeComponentCustom()
        {
            this.Text = "Bài 5.2: Bảng Tính Tiền Dịch Vụ";
            this.Size = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            // ComboBox loại dịch vụ
            Label lblCbo = new Label() { Text = "Loại dịch vụ:", Location = new Point(20, 20), AutoSize = true };
            cboCategory = new ComboBox() { Location = new Point(120, 17), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            cboCategory.SelectedIndexChanged += CboCategory_SelectedIndexChanged;

            // ListBox dịch vụ có sẵn
            Label lblAvail = new Label() { Text = "Danh sách dịch vụ:", Location = new Point(20, 60), AutoSize = true };
            lstAvailableServices = new ListBox() { Location = new Point(20, 85), Width = 220, Height = 220 };
            lstAvailableServices.DoubleClick += LstAvailableServices_DoubleClick;

            // Các nút thao tác
            btnSelect = new Button() { Text = ">", Location = new Point(255, 140), Size = new Size(70, 30) };
            btnSelect.Click += BtnSelect_Click;

            btnRemove = new Button() { Text = "<", Location = new Point(255, 180), Size = new Size(70, 30) };
            btnRemove.Click += BtnRemove_Click;

            btnClearAll = new Button() { Text = "<<", Location = new Point(255, 220), Size = new Size(70, 30) };
            btnClearAll.Click += BtnClearAll_Click;

            // ListBox dịch vụ đã chọn
            Label lblSelected = new Label() { Text = "Dịch vụ đã chọn:", Location = new Point(340, 60), AutoSize = true };
            lstSelectedServices = new ListBox() { Location = new Point(340, 85), Width = 220, Height = 220 };

            // Tổng tiền
            lblTotal = new Label() { Text = "Tổng tiền: 0 VNĐ", Location = new Point(340, 325), Font = new Font("Arial", 11, FontStyle.Bold), AutoSize = true };

            this.Controls.AddRange(new Control[] {
                lblCbo, cboCategory, lblAvail, lstAvailableServices,
                btnSelect, btnRemove, btnClearAll, lblSelected, lstSelectedServices, lblTotal
            });
        }

        private void InitData()
        {
            categories = new List<CategoryItem>
            {
                new CategoryItem {
                    CategoryName = "Khám bệnh",
                    Services = new List<Service> {
                        new Service { Name = "Khám tổng quát", Price = 200000 },
                        new Service { Name = "Khám chuyên khoa", Price = 300000 }
                    }
                },
                new CategoryItem {
                    CategoryName = "Xét nghiệm",
                    Services = new List<Service> {
                        new Service { Name = "Xét nghiệm máu", Price = 150000 },
                        new Service { Name = "Xét nghiệm nước tiểu", Price = 70000 }
                    }
                },
                new CategoryItem {
                    CategoryName = "Chụp X-Quang",
                    Services = new List<Service> {
                        new Service { Name = "X-Quang phổi", Price = 250000 },
                        new Service { Name = "X-Quang cột sống", Price = 350000 }
                    }
                },
                new CategoryItem {
                    CategoryName = "Vắc-xin",
                    Services = new List<Service> {
                        new Service { Name = "Vắc-xin cúm", Price = 200000 },
                        new Service { Name = "Vắc-xin viêm gan B", Price = 180000 }
                    }
                }
            };

            cboCategory.DataSource = categories;
        }

        private void CboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.SelectedItem is CategoryItem selectedCategory)
            {
                lstAvailableServices.DataSource = null;
                lstAvailableServices.DataSource = selectedCategory.Services;
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (lstAvailableServices.SelectedItem is Service service)
            {
                lstSelectedServices.Items.Add(service);
                CalculateTotal();
            }
        }

        private void LstAvailableServices_DoubleClick(object sender, EventArgs e)
        {
            BtnSelect_Click(sender, e);
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (lstSelectedServices.SelectedItem != null)
            {
                lstSelectedServices.Items.Remove(lstSelectedServices.SelectedItem);
                CalculateTotal();
            }
        }

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            lstSelectedServices.Items.Clear();
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (var item in lstSelectedServices.Items)
            {
                if (item is Service service) total += service.Price;
            }
            lblTotal.Text = $"Tổng tiền: {total:N0} VNĐ";
        }
    }
}