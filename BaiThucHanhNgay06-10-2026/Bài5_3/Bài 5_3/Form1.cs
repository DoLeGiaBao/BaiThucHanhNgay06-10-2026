using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bài_5_3
{
    public partial class Form1 : Form
    {
        public class Product
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public string Category { get; set; }
        }

        private TextBox txtId, txtName, txtPrice, txtQuantity, txtCategory, txtSearch;
        private Button btnAdd, btnDelete;
        private DataGridView dgvProducts;
        private BindingList<Product> productList;

        public Form1()
        {
            InitializeComponentCustom();
            InitData();
        }

        private void InitializeComponentCustom()
        {
            this.Text = "Bài 5.3: Quản Lý Danh Sách Sản Phẩm";
            this.Size = new Size(650, 520);
            this.StartPosition = FormStartPosition.CenterScreen;

            // GroupBox thông tin sản phẩm
            GroupBox grpInfo = new GroupBox() { Text = "Thông tin sản phẩm", Location = new Point(20, 15), Size = new Size(590, 140) };

            grpInfo.Controls.Add(new Label() { Text = "Mã SP:", Location = new Point(15, 30), AutoSize = true });
            txtId = new TextBox() { Location = new Point(80, 27), Width = 180 };
            grpInfo.Controls.Add(txtId);

            grpInfo.Controls.Add(new Label() { Text = "Tên SP:", Location = new Point(15, 65), AutoSize = true });
            txtName = new TextBox() { Location = new Point(80, 62), Width = 180 };
            grpInfo.Controls.Add(txtName);

            grpInfo.Controls.Add(new Label() { Text = "Đơn giá:", Location = new Point(15, 100), AutoSize = true });
            txtPrice = new TextBox() { Location = new Point(80, 97), Width = 180 };
            grpInfo.Controls.Add(txtPrice);

            grpInfo.Controls.Add(new Label() { Text = "Số lượng:", Location = new Point(310, 30), AutoSize = true });
            txtQuantity = new TextBox() { Location = new Point(380, 27), Width = 180 };
            grpInfo.Controls.Add(txtQuantity);

            grpInfo.Controls.Add(new Label() { Text = "Danh mục:", Location = new Point(310, 65), AutoSize = true });
            txtCategory = new TextBox() { Location = new Point(380, 62), Width = 180 };
            grpInfo.Controls.Add(txtCategory);

            // Chức năng & Tìm kiếm
            Label lblSearch = new Label() { Text = "Tìm kiếm (Tên):", Location = new Point(20, 175), AutoSize = true };
            txtSearch = new TextBox() { Location = new Point(120, 172), Width = 180 };
            txtSearch.TextChanged += TxtSearch_TextChanged;

            btnAdd = new Button() { Text = "Thêm", Location = new Point(410, 168), Size = new Size(90, 28) };
            btnAdd.Click += BtnAdd_Click;

            btnDelete = new Button() { Text = "Xóa", Location = new Point(510, 168), Size = new Size(90, 28) };
            btnDelete.Click += BtnDelete_Click;

            // DataGridView hiển thị sản phẩm
            dgvProducts = new DataGridView() { Location = new Point(20, 215), Size = new Size(590, 240) };
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.CellClick += DgvProducts_CellClick;

            this.Controls.AddRange(new Control[] { grpInfo, lblSearch, txtSearch, btnAdd, btnDelete, dgvProducts });
        }

        private void InitData()
        {
            productList = new BindingList<Product>
            {
                new Product { ProductId = "P01", ProductName = "Laptop Dell", UnitPrice = 15000000, Quantity = 5, Category = "Điện tử" },
                new Product { ProductId = "P02", ProductName = "Bàn phím cơ", UnitPrice = 1200000, Quantity = 10, Category = "Phụ kiện" }
            };
            dgvProducts.DataSource = productList;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = new Product
                {
                    ProductId = txtId.Text,
                    ProductName = txtName.Text,
                    UnitPrice = decimal.Parse(txtPrice.Text),
                    Quantity = int.Parse(txtQuantity.Text),
                    Category = txtCategory.Text
                };
                productList.Add(p);
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi dữ liệu đầu vào: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProducts.Rows.Count)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
                txtId.Text = row.Cells["ProductId"].Value?.ToString();
                txtName.Text = row.Cells["ProductName"].Value?.ToString();
                txtPrice.Text = row.Cells["UnitPrice"].Value?.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value?.ToString();
                txtCategory.Text = row.Cells["Category"].Value?.ToString();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null && dgvProducts.CurrentRow.DataBoundItem is Product p)
            {
                var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm [{p.ProductName}] không?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    productList.Remove(p);
                    ClearInputs();
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();
            var filtered = productList.Where(p => p.ProductName.ToLower().Contains(keyword)).ToList();
            dgvProducts.DataSource = new BindingList<Product>(filtered);
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtCategory.Clear();
            txtId.Focus();
        }
    }
}