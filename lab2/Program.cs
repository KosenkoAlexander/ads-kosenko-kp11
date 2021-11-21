/*
 * Created by SharpDevelop.
 * User: user
 * Date: 21.11.2021
 * Time: 18:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace asd_k
{
	class Program
	{
		public static int n;
		public static void Spiral(int[,]a){
			Console.WriteLine();
			int min=int.MaxValue,max=int.MinValue,mini=0,maxi=0,minj=0,maxj=0;
			int dir=0,x=n-1,y=0;
			for(int i=n-1; i>0; i--){
				for(int j=0; j<i; j++){
					if(dir==0)y++;
					else if(dir==1)x--;
					else{
						x++;
						y--;
					}
					Console.Write(a[x,y]+" ");
					if(a[x,y]>max){
						max=a[x,y];
						maxi=x;
						maxj=y;
					}
				}
				dir=(dir+1)%3;
			}
			dir=2;
			x=-1;
			y=n;
			if(n>1)Console.WriteLine("\nмаксимальний елемент пiд побiчною дiагоналлю а[{0},{1}] = {2}\n",maxi,maxj,max);
			else Console.WriteLine("пiд побiчною дiагоналлю немає елементiв");
			for(int i=n; i>0; i--){
				for(int j=0; j<i; j++){
					if(dir==0)y++;
					else if(dir==1)x--;
					else{
						x++;
						y--;
					}
					Console.Write(a[x,y]+" ");
					if(a[x,y]<min){
						min=a[x,y];
						mini=x;
						minj=y;
					}
				}
				dir=(dir+2)%3;
			}
			Console.WriteLine("\nмiнiмальний елемент над побiчною дiагоналлю включно а[{0},{1}] = {2}\n",mini,minj,min);
			return;
		}
		public static void Main(string[] args)
		{
			Console.WriteLine("введiть N");
			n=Convert.ToInt32(Console.ReadLine());
			if(n<1){
				Console.WriteLine("помилка вводу");
				Console.Write("Press any key to continue . . . ");
				Console.ReadKey(true);
				return;
			}
			Console.WriteLine("потрiбен контрольний приклад? y/n");
			if(Console.ReadLine()=="y"){
				int[,]a=new int[n,n];
				for(int i=0; i<n; i++){
					for(int j=0; j<n; j++){
						a[i,j]=j*n+i+1;
						Console.Write("{0,3} ",a[i,j]);
					}
					Console.WriteLine();
				}
				Spiral(a);
				Console.WriteLine();
			}
			int[,]b=new int[n,n];
			Random r=new Random();
			for(int i=0; i<n; i++){
				for(int j=0; j<n; j++){
					b[i,j]=r.Next(-99,100);
					Console.Write("{0,3} ",b[i,j]);
				}
				Console.WriteLine();
			}
			Spiral(b);
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}