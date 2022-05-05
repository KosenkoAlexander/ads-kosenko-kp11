/*
 * Created by SharpDevelop.
 * User: user
 * Date: 04.05.2022
 * Time: 22:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;

namespace asd_lab_6
{
	class Queue{
		string[]queue=new string[4];
		int i,j;
		public Queue(){
			for(int k=0; k<queue.Length; k++){
				queue[k]=null;
			}
			i=0;
			j=0;
		}
		public void Dequeue(){
			if(j>0){
				Console.WriteLine(queue[i]);
				queue[i]=null;
				i=(i+1)%queue.Length;
				j--;
			}else{
				Console.ForegroundColor=ConsoleColor.Red;
				Console.WriteLine("черга порожня");
				Console.ForegroundColor=ConsoleColor.Gray;
			}
		}
		public int getArrayLength(){
			return queue.Length;
		}
		public void Print(){
			Console.ForegroundColor=ConsoleColor.Blue;
			for(int k=0; k<queue.Length; k++){
				if(queue[k]==null)Console.ForegroundColor=ConsoleColor.DarkBlue;
				Console.WriteLine("[{0}{1}{2}] {3}",k,k==i?",i":"",k==(i+j-1)%queue.Length&&j>0?",j":"",queue[k]);
				Console.ForegroundColor=ConsoleColor.Blue;
			}
			Console.ForegroundColor=ConsoleColor.Gray;
		}
		public void Enqueue(string inp){
			if(j==queue.Length){
				string[]queue2=new string[queue.Length];
				Array.Copy(queue,queue2,queue.Length);
				queue=new string[queue2.Length*2];
				int i2=0;
				int i3=i;
				do{
					queue[i2]=queue2[i3];
					i2++;
					i3=(i3+1)%queue2.Length;
				}while(i3!=i);
				Console.ForegroundColor=ConsoleColor.Green;
				Console.WriteLine("масив подвоєно");
				Console.ForegroundColor=ConsoleColor.Gray;
				i=0;
				j=queue2.Length;
			}
			queue[(i+j)%queue.Length]=inp;
			j++;
		}
	}
	class Program
	{
		public static void Main(string[] args)
		{
			while(true){
				Console.WriteLine("оберiть дiю: 1 - контрольний приклад, 2 - введення з клавiатури, 3 - вихiд з програми");
				string mode=Console.ReadLine();
				if(mode=="3")return;
				if(mode=="1"){
					Queue queue=new Queue();
					queue.Print();
					for(int i=0; i<6; i++){
						Console.WriteLine();
						queue.Enqueue(i.ToString());
						queue.Print();
						Thread.Sleep(700);
					}
					for(int i=0; i<7; i++){
						Console.WriteLine();
						queue.Dequeue();
						queue.Print();
						Thread.Sleep(700);
					}
				}else if(mode=="2"){
					Console.WriteLine("введiть рядки з клавiатури, для видалення рядка використовуйте out, для виходу до меню використовуйте quit");
					string inp="";
					Queue queue=new Queue();
					do{
						queue.Print();
						inp=Console.ReadLine();
						if(inp=="out"){
							queue.Dequeue();
						}else{
							queue.Enqueue(inp);
						}
					}while(inp!="quit");
				}else{
					Console.ForegroundColor=ConsoleColor.Red;
					Console.WriteLine("помилка вводу");
					Console.ForegroundColor=ConsoleColor.Gray;
				}
			}
		}
	}
}