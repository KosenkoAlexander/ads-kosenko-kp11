/*
 * Created by SharpDevelop.
 * User: user
 * Date: 28.03.2022
 * Time: 23:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace asd4
{
	class Program
	{
		public static void Main(string[] args)
		{
			goto1:
			List<string> inp=new List<string>();
			List<string>sort1=new List<string>();
			List<string>sort2=new List<string>();
			Console.WriteLine("виберiть дiю: 1 ввести числа з клавiатури, 2 використати зразок, 3 вихiд");
			string option=Console.ReadLine();
			if(option=="1"){
				Console.WriteLine("введiть трiйковi числа, для завершення вводу натиснiть ctrl+z");
				while(true){
					string inp2=Console.ReadLine();
					bool correct=true;
					if(inp2!=null){
						for(int i=0; i<inp2.Length; i++){
							if(inp2[i]!='0'&&inp2[i]!='1'&&inp2[i]!='2'){
								correct=false;
								break;
							}
						}
						if(correct&&inp2.Length>0){
							inp.Add(inp2);
						}else{
							Console.ForegroundColor=ConsoleColor.Red;
							Console.WriteLine("помилка вводу");
							Console.ForegroundColor=ConsoleColor.Gray;
						}
					}else break;
				}
			}else if(option=="2"){
				for(int i=0; i<80; i++){
					int num=i;
					string numstr="";
					do{
						numstr=(num%3).ToString()+numstr;
						num/=3;
					}while(num>0);
					inp.Add(numstr);
				}
			}else if(option=="3"){
				return;
			}else{
				Console.ForegroundColor=ConsoleColor.Red;
				Console.WriteLine("помилка вводу");
				Console.ForegroundColor=ConsoleColor.Gray;
				goto goto1;
			}
			Console.WriteLine("невiдсортованi числа");
			int maxlen=0;
			for(int i=0; i<inp.Count; i++){
				int sum=0;
				for(int j=0; j<inp[i].Length; j++){
					sum+=Convert.ToInt32(inp[i][j].ToString())*(int)Math.Pow(3,inp[i].Length-j-1);
				}
				if(sum>Math.Pow(inp.Count,1.0/3)){
					Console.ForegroundColor=ConsoleColor.Green;
					sort1.Add(inp[i]);
				}else{
					Console.ForegroundColor=ConsoleColor.Blue;
					sort2.Add(inp[i]);
				}
				Console.Write(inp[i]+" ");
				if(inp[i].Length>maxlen)maxlen=inp[i].Length;
			}
			Console.ForegroundColor=ConsoleColor.Gray;
			Console.WriteLine("\nвiдсортованi числа");
			sort1=sort(sort1,true,maxlen-1);
			sort2=sort(sort2,false,maxlen-1);
			int index1=0,index2=0;
			for(int i=0; i<inp.Count; i++){
				int sum=0;
				for(int j=0; j<inp[i].Length; j++){
					sum+=Convert.ToInt32(inp[i][j].ToString())*(int)Math.Pow(3,inp[i].Length-j-1);
				}
				if(sum>Math.Pow(inp.Count,1.0/3)){
				   	Console.ForegroundColor=ConsoleColor.Green;
				   	Console.Write(sort1[index1]+" ");
				   	inp[i]=sort1[index1];
				   	index1++;
				}else{
					Console.ForegroundColor=ConsoleColor.Blue;
					Console.Write(sort2[index2]+" ");
					inp[i]=sort2[index2];
					index2++;
				}
			}
			Console.ForegroundColor=ConsoleColor.Gray;
			Console.WriteLine();
			goto goto1;
		}
		static List<string> sort(List<string>unsorted,bool sorttype,int digit){
			if(unsorted.Count<2||digit<0)return unsorted;
			else{
				List<string>[]unsorted2=new List<string>[3];
				for(int i=0; i<3; i++){
					unsorted2[i]=new List<string>();
				}
				for(int i=0; i<unsorted.Count; i++){
					if(unsorted[i].Length>digit){
						unsorted2[Convert.ToInt32(unsorted[i][unsorted[i].Length-digit-1].ToString())].Add(unsorted[i]);
					}else unsorted2[0].Add(unsorted[i]);
				}
				List<string>result=new List<string>();
				for(int i=0; i<3; i++){
					unsorted2[sorttype?2-i:i]=sort(unsorted2[sorttype?2-i:i],sorttype,digit-1);
					result.AddRange(unsorted2[sorttype?2-i:i]);
				}
				return result;
			}
		}
	}
}