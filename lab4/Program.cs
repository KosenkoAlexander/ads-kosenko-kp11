/*
 * Created by SharpDevelop.
 * User: user
 * Date: 23.12.2021
 * Time: 21:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Lab4
{
	class DLNode{
		public int data;
		public  DLNode next,prev;
		public DLNode(int data1, DLNode next1, DLNode prev1){
			data=data1;
			next=next1;
			prev=prev1;
		}
	}
	class Program
	{
		public static DLNode tail;
		public static void AddFirst(int data){
			if(tail==null){
				tail=new DLNode(data,null,null);
				tail.next=tail.prev=tail;
			}
			else tail.next=new DLNode(data,tail.next,tail);
		}
		public static void AddLast(int data){
			if(tail==null){
				tail=new DLNode(data,null,null);
				tail.next=tail.prev=tail;
				return;
			}
			tail.next=new DLNode(data, tail.next,tail);
			tail.next.next.prev=tail.next;
			tail=tail.next;
		}
		public static void AddAtPosition(int data, int position){
			DLNode current=tail.next;
			if(tail==null){AddFirst(data);return;}
			for(int i=0; i<position-1; i++){
				current=current.next;
				if(current==tail){
					Console.WriteLine("операцiя неможлива");
					return;
				}
			}
			if(position==0)current=current.prev;
			current.next=new DLNode(data, current.next, current);
			current.next.next.prev=current.next;
			return;
		}
		public static bool DeleteFirst(){
			if(tail==null)return true;
			if(tail.next==tail)return true;
			tail.next=tail.next.next;
			tail.next.prev=tail;
			return false;
		}
		public static bool DeleteLast(){
			if(tail==null)return true;
			if(tail.next==tail){
				tail=null;
				return false;
			}
			tail.next.prev=tail.prev;
			tail.prev.next=tail.next;
			tail=tail.prev;
			return false;
		}
		public static bool DeleteAtPosition(int position){
			if(tail==null)return true;
			DLNode current=tail.next;
			for(int i=0; i<position-1; i++){
				current=current.next;
			}
			if(position==0)current=current.prev;
			current.next=current.next.next;
			current.next.prev=current;
			return false;
		}
		public static void Print(){
			if(tail==null){
				Console.WriteLine();
				return;
			}
			DLNode current=tail.next;
			while(current!=tail){
				Console.Write(current.data+" ");
				current=current.next;
			}
			Console.Write(current.data);
			Console.WriteLine();
		}
		public static void Special(int data){
			if(tail==null){
				Console.WriteLine("операцiя неможлива");
				return;
			}
			if(tail.next==tail){
				tail.next=tail.prev=new DLNode(data,tail,tail);
				return;
			}
			if(data%2==0){
				tail.next.next=new DLNode(data,tail.next.next,tail.next);
				tail.next.next.next.prev=tail.next.next;
			}else{
				tail.next=new DLNode(data,tail.next,tail);
				tail.next.next.prev=tail.next;
			}
		}
		public static void Main(string[] args)
		{
			tail=null;
			while(true){
				Console.WriteLine("введiть команду:\nAddFirst-додати у голову\nAddLast-додати у хвiст\nAddAtPosition-додати на визначену позицiю\nDeleteFirst-видалити голову\nDeleteLast-видалити хвiст\nDeleteAtPosition-видалити з позицii\nPrint-вивести вмiст\nExit-завершити роботу\nSpecial-спецiальна функцiя");
				string command=Console.ReadLine();
				if(command=="AddFirst"){
					Console.WriteLine("введiть число");
					AddFirst(Convert.ToInt32(Console.ReadLine()));
				}
				if(command=="AddLast"){
					Console.WriteLine("введiть число");
					AddLast(Convert.ToInt32(Console.ReadLine()));
				}
				if(command=="AddAtPosition"){
					Console.WriteLine("введiть число");
					int num=Convert.ToInt32(Console.ReadLine());
					Console.WriteLine("введiть позицiю");
					AddAtPosition(num,Convert.ToInt32(Console.ReadLine()));
				}
				if(command=="DeleteFirst"){
					if(DeleteFirst())Console.WriteLine("операцiя неможлива");
				}
				if(command=="DeleteLast"){
					if(DeleteLast())Console.WriteLine("операцiя неможлива");
				}
				if(command=="DeleteAtPosition"){
					Console.WriteLine("введiть позицiю");
					if(DeleteAtPosition(Convert.ToInt32(Console.ReadLine())))Console.WriteLine("операцiя неможлива");
				}
				if(command=="Print"){
					Print();
				}
				if(command=="Exit"){
					return;
				}
				if(command=="Special"){
					Console.WriteLine("введiть число");
					Special(Convert.ToInt32(Console.ReadLine()));
				}
			}
		}
	}
}