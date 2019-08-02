using DisplayBLL;
using Model;
using System;
using System.Windows.Forms;

namespace WF
{
    public partial class TestMenue : BaseForm
    {
        MenueBLL bll = new MenueBLL();
        public TestMenue()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void TestMenue_Load(object sender, EventArgs e)
        {
            InitialTree();
        }

        //初始化菜单 获取一级菜单
        public void InitialTree()
        {
            treeView1.Nodes.Clear();
            foreach (var item in bll.GetFirstMenue())
            {
                TreeNode node = new TreeNode(item.name);
                node.Tag = item.id;
                treeView1.Nodes.Add(RecursiveGetNode(node));
            }
        }

        //递归获取多层子菜单
        public TreeNode RecursiveGetNode(TreeNode node)
        {
            if (bll.HasChild(node.Tag))
            {
                foreach (var item in bll.GetChild(node.Tag))
                {
                    TreeNode newNode = new TreeNode(item.name);
                    newNode.Tag = item.id;
                    node.Nodes.Add(RecursiveGetNode(newNode));
                }
            }
            return node;
        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {

        }

        //添加一级菜单
        private void button1_Click(object sender, EventArgs e)
        {
            MenueModel model = new MenueModel()
            {
                name = textBox1.Text,
                parentId = 0
            };
            if (model.name.Trim() == "")
            {
                Show("菜单名称不能为空");
                return;
            }

            if (bll.AddNode(model))
            {
                InitialTree();
                textBox1.Text = "";
                Show("添加成功菜单 " + model.name + " 成功");
            }
            else
            {
                Show("添加成功菜单 " + model.name + " 失败");
            }
        }

        //添加子菜单
        private void button2_Click(object sender, EventArgs e)
        {
            TreeNode node = GetSeletNode();
            if (node == null)
                return;

            MenueModel model = new MenueModel()
            {
                name = textBox1.Text,
                parentId = int.Parse(node.Tag.ToString())
            };

            if (model.name.Trim() == "")
            {
                Show("菜单名称不能为空");
                return;
            }

            if (bll.AddNode(model))
            {
                textBox1.Text = "";
                Show("添加成功菜单 " + model.name + " 成功");
            }
            else
            {
                Show("添加成功菜单 " + model.name + " 失败");
            }
        }

        //获取选中的节点
        public TreeNode GetSeletNode()
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null)
            {
                Show("请先选中一个节点");
                return null;
            }
            else
                return node;
        }

        //删除节点
        private void button3_Click(object sender, EventArgs e)
        {
            TreeNode node = GetSeletNode();
            if (node == null)
                return;

            if (Ask("是否删除 " + node.Text) == DialogResult.OK)
            {
                if (bll.DeleteNode(node.Tag))
                {
                    InitialTree();
                    Show("删除 " + node.Text + " 成功");
                }
                else
                {
                    Show("删除 " + node.Text + " 失败");
                }
            }


        }
    }
}
