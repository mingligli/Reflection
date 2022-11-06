using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
//    从概念上可以区分字段和属性的区别，字段是一个用于存储数据的变量，属性是一个方法或者说是一个函数成员。那么既然属性是一个方法，那么它和方法的区别是什么呢？

//从定义就可以看出来，属性其实本身就是方法。但既然把属性和方法定义成为两个概念，那么它们之间肯定有不同的地方。个人感觉，他们最大的区别首先是属性没有参数列表，而方法必须要有参数列表，哪怕没有参数，也要放一个空括号在那。其次，属性定义里要有set和get两个访问器，用于获得属性的值和设定属性的值。其余的地方没有发现不同之处，属性里也可以判定数据的合法性，和方法没有什么两样。

    //定义类
    public class MyClass
    {
        public readonly string  Property ="我是原来的公开的只读字段";//字段
        public int Property1 { get ; set ; } //类的属性，对字段get,set

       public static string say(string name)
        {
            return name +@":这是方法里的文字";
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            MyClass tmp_Class = new MyClass();
            tmp_Class.Property1 = 2;
            Type type = tmp_Class.GetType(); //获取类型

            //1、获取指定名称的属性  
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Property1");           
            int value_Old = (int)propertyInfo.GetValue(tmp_Class, null); //获取属性值
            Console.WriteLine(@"直接调用对象写入的数：" + value_Old);
            propertyInfo.SetValue(tmp_Class, 5, null); //给对应属性赋值
            int value_New = (int)propertyInfo.GetValue(tmp_Class, null);
            Console.WriteLine(@"用反射赋值后的数：" + value_New);

            //2、获取指定名称的字段,（私有的读不到，只读的都可以修改）
            System.Reflection.FieldInfo fieldinfo = type.GetField("Property");
           Console.WriteLine("改前："+ fieldinfo.GetValue(tmp_Class));
            fieldinfo.SetValue(tmp_Class, "调用反射修改后的字段");
            Console.WriteLine("改后：" + fieldinfo.GetValue(tmp_Class));

            //3、获取指定名称的方法,更多见https://blog.csdn.net/Upgrader/article/details/106795440
            Console.WriteLine( MyClass.say("直接调用方法"));
            System.Reflection.MethodInfo methodInfo = type.GetMethod("say");
            Console.WriteLine( methodInfo.Invoke(tmp_Class, new object[] { "反射调用"}));

            Console.ReadKey();
        }
    }
}
