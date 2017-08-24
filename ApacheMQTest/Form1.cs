using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Apache.NMS.ActiveMQ;
using Apache.NMS;
using System.Diagnostics;

namespace ApacheMQTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitProducer();
            InitProducerAndConsumer();
        }
        static  private IConnectionFactory factory;
        static IMessageProducer prod1;
        static ITextMessage message;
        static IMessageConsumer consumer;
        public void InitProducer()
        {
            try
            {
                //初始化工厂，这里默认的URL是不需要修改的
                factory = new ConnectionFactory("tcp://localhost:61616");

            }
            catch
            {
                lblMessage.Text = "初始化失败!!";
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {


            //给这个对象赋实际的消息
            message.Text = txtMessage.Text;
            //设置消息对象的属性，这个很重要哦，是Queue的过滤条件，也是P2P消息的唯一指定属性
            message.Properties.SetString("filter1", "demo1");
            //生产者把消息发送出去，几个枚举参数MsgDeliveryMode是否长链，MsgPriority消息优先级别，发送最小单位，当然还有其他重载
            prod1.Send(message, MsgDeliveryMode.NonPersistent, MsgPriority.Normal, TimeSpan.MinValue);
            lblMessage.Text = "发送成功!!";
            txtMessage.Text = "";
            txtMessage.Focus();
        }
        /// <summary>
        /// 创建消费者和生产者
        /// </summary>
        public void InitProducerAndConsumer()
        {
            //通过工厂建立连接
            IConnection connection = factory.CreateConnection();

            //通过连接创建Session会话
            ISession session = connection.CreateSession();

            //通过会话创建生产者，方法里面new出来的是MQ中的Queue
            prod1 = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("firstQueue"));
            //创建一个发送的消息对象
            message = prod1.CreateTextMessage();


            //-------------创建消费者
            consumer = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("returnQueue"), "userId='SC100410'");
            consumer.Listener += new MessageListener(Consumer_Listener);
            connection.Start();


        }
        private void Consumer_Listener(IMessage message)
        {
            ITextMessage msg = (ITextMessage)message;
            if (this.InvokeRequired)
            {
                this.Invoke((Action)delegate()
                {
                    txtResult.Text +=DateTime.Now.ToString()+"接收："+ msg.Text +"\r\n";
                });
            }
            else
            {
                txtResult.Text += DateTime.Now.ToString() + "接收：" + msg.Text + "\r\n";
            }
        }
    }
}

