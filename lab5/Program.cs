/*
 * Created by SharpDevelop.
 * User: user
 * Date: 06.03.2022
 * Time: 14:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace asd_lab_5
{
	class Program
	{
		public static List<string>unsorted=new List<string>();
		public static void Main(string[] args)
		{
			while(true){
				unsorted.Clear();
				Console.ForegroundColor=ConsoleColor.Gray;
				Console.WriteLine("введiть цифру щоб вибрати:\n1 введення з клавiатури\n2 контрольний приклад\n3 вихiд");
				int option=Convert.ToInt32(Console.ReadLine());
				if(option==1){
					Console.WriteLine("введiть строки формату Х0000ХХ, де Х-малi латинськi лiтери, а 0-цифри, для завершення вводу натиснiть ctrl Z");
					string inp;
					while(true){
						inp=Console.ReadLine();
						if(inp==null)break;
						if(inp.Length==7){
							bool t=true;
							for(int i=0; i<7; i++){
								if(i>0&&i<5){
									if(!Char.IsNumber(inp[i]))t=false;
								}else{
									if((int)inp[i]<97||(int)inp[i]>122)t=false;
								}
							}
							foreach(string str in unsorted){
								if(inp==str)t=false;
							}
							if(t)unsorted.Add(inp);
							else Console.WriteLine("помилка вводу");
						}else Console.WriteLine("помилка вводу");
					}
					initializesort();
				}else if(option==2){
					Console.WriteLine("контрольний приклад");
					Random r=new Random();
					List<string>generated=new List<string>();
					for(int i=0; i<5; i++){
						for(int j=0; j<5; j++){
							generated.Add((char)(97+i)+j.ToString()+"000xx");
						}
					}
					for(int i=0; i<25; i++){
						int j=r.Next(0,generated.Count);
						unsorted.Add(generated[j]);
						generated.RemoveAt(j);
					}
					initializesort();
				}else if(option==3){
					return;
				}else Console.WriteLine("помилка вводу");
			}
		}
		public static void initializesort(){
			Console.WriteLine("невiдсортована колекцiя");
			foreach(string str in unsorted){
				for(int i=0; i<7; i++){
					if(i>0&&i<5)Console.ForegroundColor=ConsoleColor.Green;
					else Console.ForegroundColor=ConsoleColor.White;
					Console.Write(str[i]);
				}
				Console.WriteLine();
			}
			Console.ForegroundColor=ConsoleColor.Gray;
			Console.WriteLine("вiдсортована колекцiя");
			foreach(string str in sort(0,unsorted)){
				for(int i=0; i<7; i++){
					if(i>0&&i<5)Console.ForegroundColor=ConsoleColor.Green;
					else Console.ForegroundColor=ConsoleColor.White;
					Console.Write(str[i]);
				}
				Console.WriteLine();
			}
		}
		public static List<string> sort(int index,List<string>collection){
			if(index==7||collection.Count==0)return collection;
			if(index>0&&index<5){
				List<string>[]split=new List<string>[10];
				for(int i=0; i<10; i++){
					split[i]=new List<string>();
				}
				foreach(string str in collection){
					split[(int)str[index]-48].Add(str);
				}
				collection.Clear();
				for(int i=0; i<10; i++){
					collection.AddRange(sort(index+1,split[9-i]));
				}
				return collection;
			}else{
				List<string>[]split=new List<string>[26];
				for(int i=0; i<26; i++){
					split[i]=new List<string>();
				}
				foreach(string str in collection){
					split[(int)str[index]-97].Add(str);
				}
				collection.Clear();
				for(int i=0; i<26; i++){
					collection.AddRange(sort(index+1,split[25-i]));
				}
				return collection;
			}
		}
	}
}