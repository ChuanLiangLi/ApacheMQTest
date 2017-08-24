using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace ApacheMQConsumer
{
    public partial class Form1 : Form
    {
        static IMessageProducer prod1;
        static ITextMessage message;
        public Form1()
        {
            InitializeComponent();
            InitConsumer();
        }
        public void InitConsumer()
        {
            //创建连接工厂
            IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616");
            //通过工厂构建连接
            IConnection connection = factory.CreateConnection();
            //这个是连接的客户端名称标识
            connection.ClientId = "firstQueueListener";
            //启动连接，监听的话要主动启动连接
            connection.Start();
            //通过连接创建一个会话
            ISession session = connection.CreateSession();
            //通过会话创建一个消费者，这里就是Queue这种会话类型的监听参数设置
            IMessageConsumer consumer = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("firstQueue"), "filter1='demo1'");
            //注册监听事件
            consumer.Listener += new MessageListener(Consumer_Listener);

            //2.      --------------创建生产者
             //通过会话创建生产者，方法里面new出来的是MQ中的Queue
             prod1 = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("returnQueue"));
             //创建一个发送的消息对象
            message = prod1.CreateTextMessage();
            message.Properties.SetString("userId", "SC100410");
        }

        public void Consumer_Listener(IMessage message)
        {
            ITextMessage txtMsg = (ITextMessage)message;
            lblReceiveMessage.Invoke(new DelegateRevMessage(RevMessage),txtMsg);
            //lblReceiveMessage.Invoke((Action<ITextMessage>)((t) =>
            //{
            //    lblReceiveMessage.Text += string.Format(@"接收到:{0}{1}", t.Text, Environment.NewLine);
            //}),txtMsg);
        }
       
        public delegate void DelegateRevMessage(ITextMessage messag);
        public void RevMessage(ITextMessage message)
        {
            lblReceiveMessage.Text += string.Format(@"接收到:{0}{1}",message.Text,Environment.NewLine);
            DealMessge(message.Text);
        }
        public void DealMessge(string str)
        {
            //给这个对象赋实际的消息
            message.Text = str + "{\"status\":\"success\"}" ;
           
            //生产者把消息发送出去，几个枚举参数MsgDeliveryMode是否长链，MsgPriority消息优先级别，发送最小单位，当然还有其他重载
            prod1.Send(message, MsgDeliveryMode.NonPersistent, MsgPriority.Normal, TimeSpan.MinValue);
        }

    }
}
