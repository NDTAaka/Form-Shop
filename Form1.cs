using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Form_Shop
{
    public partial class Form1 : Form
    {
        private List<Product> _productList = new List<Product>();
        private ShoppingCart _shoppingCart = new ShoppingCart();

        public Form1()
        {
            InitializeComponent();
            LoadProducts();
            UpdateProductList();
        }

        private void LoadProducts()
        {
            // Tạo một vài sản phẩm mẫu
            _productList.Add(new Product("Chuot", 100, 1));
            _productList.Add(new Product("Ban Phim", 200, 1));
        }

        private void UpdateProductList()
        {
            dataGridView1.Rows.Clear();
            foreach (var product in _productList)
            {
                dataGridView1.Rows.Add( product.Name, product.Price, product.Quantity);
            }
        }
        public class Product
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }

            public Product(string name, decimal price, int quantity)
            {
                Name = name;
                Price = price;
                Quantity = quantity;
            }
        }
        public class ShoppingCart
        {
            private List<Product> _products = new List<Product>();

            public void AddProduct(Product product)
            {
                _products.Add(product);
            }

            public void RemoveProduct(Product product)
            {
                _products.Remove(product);
            }

            public decimal CalculateTotal()
            {
                return _products.Sum(p => p.Price * p.Quantity);
            }

            public List<Product> GetProducts()
            {
                return _products;
            }

            public void ClearCart()
            {
                _products.Clear();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedRow = dataGridView1.CurrentRow;
                var selectedProduct = _productList[dataGridView1.CurrentRow.Index];

                // Thêm sản phẩm vào giỏ hàng
                _shoppingCart.AddProduct(new Product(selectedProduct.Name, selectedProduct.Price, 1));
                UpdateCart();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                var selectedProduct = _shoppingCart.GetProducts()[dataGridView2.CurrentRow.Index];
                _shoppingCart.RemoveProduct(selectedProduct);
                UpdateCart();
            }
        }
        private void UpdateCart()
        {
            dataGridView2.Rows.Clear();
            foreach (var product in _shoppingCart.GetProducts())
            {
                dataGridView2.Rows.Add( product.Name, product.Price, product.Quantity);
            }

            labelTotal.Text = $"Tổng giá trị: {_shoppingCart.CalculateTotal():C}";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thanh toán thành công! Cảm ơn bạn đã mua hàng.");
            _shoppingCart.ClearCart();
            UpdateCart();
        }
    }
}
