/*
 * Created by SharpDevelop.
 * User: user
 * Date: 05.12.2021
 * Time: 18:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace asd8
{
	class Program
	{
		public static bool function(int x, int y){
			int output=Math.Max(x,y);
			while(true){
				if(output%x==0&&output%y==0){
					return output%2!=0;
				}
				output++;
			}
		}
		public static void Main(string[] args)
		{
			Console.WriteLine("введiть N, M, K");
			int n=Convert.ToInt32(Console.ReadLine());
			int m=Convert.ToInt32(Console.ReadLine());
			int k=Convert.ToInt32(Console.ReadLine());
			int[,]a=new int[n,m];
			List<int>a2=new List<int>();
			Random r=new Random();
			for(int i=0; i<n; i++){
				for(int j=0; j<m; j++){
					a[i,j]=r.Next(-999,1000);
					if(function(j+1,k))Console.BackgroundColor=ConsoleColor.Red;
					Console.Write("{0,4} ",a[i,j]);
					Console.BackgroundColor=ConsoleColor.Black;
				}
				Console.WriteLine();
			}
			for(int j=0; j<m; j++){
				if(function(j+1,k))a2.Add(j);
			}
			for(int i=0; i<n*a2.Count-1; i++){
				bool t=true;
				for(int j=0; j<n*a2.Count-i-1; j++){
					if(a[n-j%n-1,a2[j/n]]<a[n-(j+1)%n-1,a2[(j+1)/n]]){
						int a3=a[n-(j+1)%n-1,a2[(j+1)/n]];
						a[n-(j+1)%n-1,a2[(j+1)/n]]=a[n-j%n-1,a2[j/n]];
						a[n-j%n-1,a2[j/n]]=a3;
						t=false;
					}
				}
				if(t)break;
			}
			Console.BackgroundColor=ConsoleColor.Black;
			Console.WriteLine();
			for(int i=0; i<n; i++){
				for(int j=0; j<m; j++){
					if(function(j+1,k))Console.BackgroundColor=ConsoleColor.Red;
					Console.Write("{0,4} ",a[i,j]);
					Console.BackgroundColor=ConsoleColor.Black;
				}
				Console.WriteLine();
			}
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}