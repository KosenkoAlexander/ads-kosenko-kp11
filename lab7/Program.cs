/*
 * Created by SharpDevelop.
 * User: user
 * Date: 11.06.2022
 * Time: 18:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace ads_lab7
{
	class TimeDate{
		public int year,day,timeH,timeM;
		public string month;
		public TimeDate(int year,string month,int day,int timeH,int timeM){
			this.year=year;
			this.month=month;
			this.day=day;
			this.timeH=timeH;
			this.timeM=timeM;
		}
		public static string[] monthnames={"сiчень","лютий","березень","квітень","травень","червень","липень","серпень","вересень","жовтень","листопад","грудень"};
		public static int[] monthlength={31,28,31,30,31,30,31,31,30,31,30,31};
		public static int findmonthlength(int year,string month){
			int monthID=Array.IndexOf(monthnames,month);
			if(monthID==1){
				if(year%100==0){
					if(year%400==0){
						return 29;
					}else{
						return 28;
					}
				}else if(year%4==0){
					return 29;
				}else{
					return 28;
				}
			}
			return monthlength[monthID];
		}
		public static TimeDate addminutes(int minutes,TimeDate addTo){
			
			TimeDate newdate=new TimeDate(addTo.year,addTo.month,addTo.day,addTo.timeH,addTo.timeM);
			if(minutes>0){
				newdate.timeM+=minutes;
				newdate.timeH+=newdate.timeM/60;
				newdate.timeM%=60;
				newdate.day+=newdate.timeH/24;
				newdate.timeH%=24;
				while(newdate.day>findmonthlength(newdate.year,newdate.month)){
					newdate.day-=findmonthlength(newdate.year,newdate.month);
					if(Array.IndexOf(monthnames,newdate.month)==11){
						newdate.year++;
						newdate.month=monthnames[0];
					}else{
						newdate.month=monthnames[1+Array.IndexOf(monthnames,newdate.month)];
					}
				}
			}else{
				newdate.timeM+=minutes;
				if(newdate.timeM<0){
					newdate.timeH-=1+(-newdate.timeM)/60;
					newdate.timeM=60-(-newdate.timeM)%60;
					if(newdate.timeH<0){
						newdate.day-=1+(-newdate.timeH)/24;
						while(newdate.day<=0){
							newdate.day+=findmonthlength(newdate.year,monthnames[(Array.IndexOf(monthnames,newdate.month)-1)%12]);
							if(Array.IndexOf(monthnames,newdate.month)==0){
								newdate.month=monthnames[11];
								newdate.year--;
							}else{
								newdate.month=monthnames[Array.IndexOf(monthnames,newdate.month)-1];
							}
						}
					}
				}
			}
			return newdate;
		}
		public static int timeDateCompare(TimeDate t1, TimeDate t2){
			if(t1.year>t2.year)return 1;
			if(t1.year<t2.year)return -1;
			if(Array.IndexOf(monthnames,t1.month)>Array.IndexOf(monthnames,t2.month))return 1;
			if(Array.IndexOf(monthnames,t1.month)<Array.IndexOf(monthnames,t2.month))return -1;
			if(t1.day>t2.day)return 1;
			if(t1.day<t2.day)return-1;
			if(t1.timeH>t2.timeH)return 1;
			if(t1.timeH<t2.timeH)return -1;
			if(t1.timeM>t2.timeM)return 1;
			if(t1.timeM<t2.timeM)return -1;
			return 0;
		}
	}
	class Key{
		public string flightCode;
		public Key(string s){
			flightCode=s;
		}
	}
	class Value{
		public string aeroportOfArrival,gate;
		public TimeDate departureTime;
		public int isDelayedH=0,isDelayedM=0;
		public Value(string aeroport,string gate,int year,string month,int day,int timeH,int timeM){
			aeroportOfArrival=aeroport;
			this.gate=gate;
			departureTime=new TimeDate(year,month,day,timeH,timeM);
		}
	}
	class Entry{
		public Key key;
		public Value value;
		public bool DELETED;
		public Entry(){
			DELETED=false;
		}
	}
	class Hashtable{
		public Entry[] table;
		public int loadness;
		public int size;
		public Hashtable(int size){
			this.size=size;
			table=new Entry[size];
			for(int i=0; i<size; i++){
				table[i]=new Entry();
			}
			loadness=0;
		}
		public int hashCode(Key key){
			int sum=0;
			for(int i=0; i<key.flightCode.Length; i++){
				sum+=(int)key.flightCode[i];
			}
			return sum%size;
		}
		public int getHash(Key key){
			int hashcode=hashCode(key);
			while(table[hashcode].key!=null&&table[hashcode].key.flightCode!=key.flightCode){
				hashcode=(hashcode+1)%size;
			}
			return hashcode;
		}
		public void insertEntry(Key key,Value value){
			int place=getHash(key);
			table[place].key=key;
			table[place].value=value;
			loadness++;
			if(loadness==size){
				rehashing();
			}
		}
		public void removeEntry(Key key){
			Entry e=findEntry(key);
			if(e!=null){
				e.key=null;
				e.value=null;
				e.DELETED=true;
				loadness--;
			}
		}
		public Entry findEntry(Key key){
			int hashcode=hashCode(key),inithashcode=hashcode;
			while(true){
				if(table[hashcode].key==null&&!table[hashcode].DELETED){
					return null;
				}else if(table[hashcode].key.flightCode==key.flightCode){
					return table[hashcode];
				}else if((hashcode+1)%size==inithashcode){
					return null;
				}
				hashcode=(hashcode+1)%size;
			}
		}
		public void rehashing(){
			Console.WriteLine("розмiр геш-таблицi подвоєно");
			Entry[] table2=new Entry[size];
			Array.Copy(table,table2,size);
			size=size*2;
			loadness=0;
			table=new Entry[size];
			for(int i=0; i<size; i++){
				table[i]=new Entry();
			}
			for(int i=0; i<size/2; i++){
				if(table2[i].key!=null){
					insertEntry(table2[i].key,table2[i].value);
				}
			}
		}
		public void setDelay(Key code,int isDelayedH, int isDelayedM){
			Entry e=findEntry(code);
			if(e!=null){
				e.value.isDelayedH=isDelayedH;
				e.value.isDelayedM=isDelayedM;
			}
		}
		
	}
	class Entry2{
		public string key;
		public bool DELETED;
		public List<string> value;
		public Entry2(string k){
			key=k;
			value=new List<string>();
			DELETED=false;
		}
		public Entry2(){
			key=null;
			value=null;
			DELETED=false;
		}
	}
	class additionalHashtable{
		public int size,used;
		public Entry2[] hashtable;
		public additionalHashtable(int size){
			this.size=size;
			hashtable=new Entry2[size];
			for(int i=0; i<hashtable.Length; i++){
				hashtable[i]=new Entry2();
			}
			used=0;
		}
		public int getHashCode(string value){
			int sum=0;
			for(int i=0; i<value.Length; i++){
				sum+=(int)value[i];
			}
			return sum%size;
		}
		public void insertEntry(Entry2 e){
			int hash=getHashCode(e.key);
			while(true){
				if(hashtable[hash].key==null){
					hashtable[hash].key=e.key;
					hashtable[hash].value=e.value;
					used++;
					return;
				}
				hash=(hash+1)%size;
			}
		}
		public int searchEntry(string str){
			int hash=getHashCode(str);
			for(int i=0; i<size; i++){
				if(hashtable[hash].key==str){
					return hash;
				}
				if(hashtable[hash].key==null&&!hashtable[hash].DELETED){
					return -1;
				}
				hash=(hash+1)%size;
			}
			return -1;
		}
		public void removeEntry(string str){
			int hash=searchEntry(str);
			if(hash!=-1){
				hashtable[hash].key=null;
				hashtable[hash].value=null;
				hashtable[hash].DELETED=true;
				used--;
			}
		}
		public string Add(Key flight, string gate, TimeDate td, Hashtable table){
			int hash=searchEntry(gate);
			if(hash!=-1){
				if(hashtable[hash].value.Count==0){
					hashtable[hash].value.Add(flight.flightCode);
					return gate;
				}else{
					int ID=-1;
					TimeDate time=td;
					for(int i=0; i<hashtable.Length; i++){
						if(hashtable[i].key!=null){
							if(hashtable[i].value.Count==0){
								hashtable[i].value.Add(flight.flightCode);
								return hashtable[i].key;
							}
							for(int j=0; j<hashtable[i].value.Count; j++){
								Key key=new Key(hashtable[i].value[j]);
								if(TimeDate.timeDateCompare(table.findEntry(key).value.departureTime,time)<=0){
									time=table.findEntry(key).value.departureTime;
									ID=i;
								}
							}
						}
					}
					if(ID!=-1){
						hashtable[ID].value.Add(flight.flightCode);
						return hashtable[ID].key;
					}
					hashtable[searchEntry(gate)].value.Add(flight.flightCode);
					return hashtable[searchEntry(gate)].key;
				}
			}else{
				if(used==size){
					Console.WriteLine("не можливо додати гейт. Змiнiть кiлькiсть гейтiв. Дiю вiдмiнено");
					return null;
				}else{
					Entry2 item=new Entry2(gate);
					insertEntry(item);
					this.Add(flight,gate,td,table);
					return gate;
				}
			}
		}
		public void rehash(int newsize){
			if(used<=newsize){
				Entry2[] newtable=new Entry2[size];
				Array.Copy(hashtable,newtable,size);
				size=newsize;
				hashtable=new Entry2[size];
				for(int i=0; i<size; i++){
					hashtable[i]=new Entry2();
				}
				for(int i=0; i<newtable.Length; i++){
					this.insertEntry(newtable[i]);
				}
			}else{
				Console.WriteLine("змiна розміру таблицi призведе до втрати даних, дiю вiдмiнено");
			}
		}
		public void remove(Key flight,Hashtable table){
			if (table.findEntry(flight)==null)return;
			if(searchEntry(table.findEntry(flight).value.gate)!=-1){
				hashtable[searchEntry(table.findEntry(flight).value.gate)].value.Remove(flight.flightCode);
				if(hashtable[searchEntry(table.findEntry(flight).value.gate)].value.Count==0){
					this.removeEntry(table.findEntry(flight).value.gate);
				}
			}
		}
		public int decideDelay(Key KEY,Value value,Hashtable table){
			bool b=true;
			TimeDate max=TimeDate.addminutes(-105,value.departureTime);
			for(int i=0; i<hashtable[this.searchEntry(value.gate)].value.Count; i++){
				if(hashtable[this.searchEntry(value.gate)].value[i]!=KEY.flightCode){
					Key key=new Key(hashtable[this.searchEntry(value.gate)].value[i]);
					if(TimeDate.timeDateCompare(TimeDate.addminutes(105,value.departureTime),TimeDate.addminutes(table.findEntry(key).value.isDelayedH*60+table.findEntry(key).value.isDelayedM,table.findEntry(key).value.departureTime))==1&&TimeDate.timeDateCompare(TimeDate.addminutes(-105,value.departureTime),TimeDate.addminutes(table.findEntry(key).value.isDelayedH*60+table.findEntry(key).value.isDelayedM,table.findEntry(key).value.departureTime))==-1){
						b=false;
						if(TimeDate.timeDateCompare(max,TimeDate.addminutes(table.findEntry(key).value.isDelayedH*60+table.findEntry(key).value.isDelayedM,table.findEntry(key).value.departureTime))==-1){
							max=TimeDate.addminutes(table.findEntry(key).value.isDelayedH*60+table.findEntry(key).value.isDelayedM,table.findEntry(key).value.departureTime);
						}
					}
				}
			}
			if(b){
				return 0;
			}
			else{
				max=TimeDate.addminutes(105,max);
				int result = max.timeH*60+max.timeM-value.departureTime.timeH*60-value.departureTime.timeM;
				if(result<0)return (1440+result)%1440;
				return result;
			}
		}
	}
	class Program
	{
		public static int gateNumber=5;
		public static void insert(string flightcode,string aeroport, string gate,int year,string month,int day,int hours,int minutes,Hashtable table,additionalHashtable table2){
			Key key=new Key(flightcode);
			TimeDate td=new TimeDate(year,month,day,hours,minutes);
			gate=table2.Add(key,gate,td,table);
			if(gate==null)return;
			Value value=new Value(aeroport,gate,year,month,day,hours,minutes);
			table.insertEntry(key,value);
			int d=table2.decideDelay(key,value,table);
			table.setDelay(key,d/60,d%60);
		}
		public static void Main(string[] args)
		{
			Hashtable table=new Hashtable(5);
			additionalHashtable table2=new additionalHashtable(5);
			while(true){
				Console.WriteLine("оберiть дiю:\n1 заповнити таблицю контрольними значеннями\n2 додати новий елемент\n3 видалити елемент за ключем\n4 знайти елемент за ключем\n5 вивести геш-таблицю\n6 шукати всi рейси, що вилiтають з заданого гейту\n7 вихiд\n8 змiнити кiлькiсть гейтiв (за замовчуванням 5)");
				string mode=Console.ReadLine();
				if(mode=="1"){
					insert("aaaa","Київський аеропорт","a",2022,"червень",20,12,30,table,table2);
					insert("бббб","Запорізький аеропорт","a",2022,"червень",20,12,35,table,table2);
					insert("вввв","Львівський аеропорт","б",2022,"червень",19,15,20,table,table2);
					insert("гггг","Київський аеропорт","б",2022,"червень",18,18,30,table,table2);
					insert("дддд","Київський аеропорт","в",2022,"червень",16,10,50,table,table2);
					insert("аб","Львівський аеропорт","г",2022,"червень",20,10,45,table,table2);
					insert("ба","Київський аеропорт","д",2022,"червень",12,12,55,table,table2);
					Console.WriteLine("додано контрольні значення");
				}else if(mode=="2"){
					Console.WriteLine("введiть код польоту");
					string flightcode=Console.ReadLine();
					Console.WriteLine("введiть аеропорт прибуття");
					string aeroport=Console.ReadLine();
					Console.WriteLine("введiть гейт за замовчуванням\nгейт буде додано до вiдповiдної таблицi якщо його ще немає i не перевищено лiмiт");
					string gate=Console.ReadLine();
					Console.WriteLine("введiть рiк вiдправки");
					int year;
					try{
						year=Convert.ToInt32(Console.ReadLine());
					}catch{
						Console.WriteLine("помилка вводу, неправильний формат");
						continue;
					}
					Console.WriteLine("введiть мiсяць вiдправки зi списку");
					foreach(string str in TimeDate.monthnames){
						Console.WriteLine(str);
					}
					string month=Console.ReadLine();
					int monthID=Array.IndexOf(TimeDate.monthnames,month);
					if(monthID<0){
						Console.WriteLine("неправильна назва мiсяця");
						continue;
					}
					Console.WriteLine("введiть день вiдправки");
					int day;
					try{
						day=Convert.ToInt32(Console.ReadLine());
					}catch{
						Console.WriteLine("помилка вводу, неправильний формат");
						continue;
					}
					if(day<1||day>TimeDate.monthlength[monthID]){
						Console.WriteLine("помилка вводу, неправильна дата");
					}
					Console.WriteLine("введiть час вiдправки у форматi hh:mm");
					string time=Console.ReadLine();
					if(!Char.IsDigit(time[0])&&Char.IsDigit(time[1])&&Char.IsDigit(time[3])&&Char.IsDigit(time[4])&&time[2]==':'){
						Console.WriteLine("помилка вводу, неправильний формат");
						continue;
					}
					int hours=Convert.ToInt32(time.Substring(0,2));
					int minutes=Convert.ToInt32(time.Substring(3,2));
					if(hours>24||minutes>60){
						Console.WriteLine("помилка вводу, неправильний час");
						continue;
					}
					insert(flightcode,aeroport,gate,year,month,day,hours,minutes,table,table2);
				}else if(mode=="3"){
					Console.WriteLine("введіть ключ");
					string str=Console.ReadLine();
					Key key=new Key(str);
					table2.remove(key,table);
					table.removeEntry(key);
				}else if(mode=="4"){
					Console.WriteLine("введiть ключ");
					string str=Console.ReadLine();
					Key key=new Key(str);
					Value item=table.findEntry(key).value;
					if(item!=null)Console.WriteLine("гейт "+item.gate+"\nстарт "+item.departureTime.day+", "+item.departureTime.month+" "+item.departureTime.year+" "+item.departureTime.timeH+":"+item.departureTime.timeM+"\nвідкладено на "+item.isDelayedH+":"+item.isDelayedM);
					else Console.WriteLine("не iснує");
				}else if(mode=="5"){
					int i=0;
					foreach(Entry item in table.table){
						Console.Write("["+i+"]");
						if(item.key!=null)Console.WriteLine("код "+item.key.flightCode+"\nаеропорт "+item.value.aeroportOfArrival+"\nгейт "+item.value.gate+"\nстарт "+item.value.departureTime.day+", "+item.value.departureTime.month+" "+item.value.departureTime.year+" "+item.value.departureTime.timeH+":"+item.value.departureTime.timeM+"\nвiдкладено на "+item.value.isDelayedH+":"+item.value.isDelayedM);
						else if(item.DELETED)Console.WriteLine("[DELETED]");
						else Console.WriteLine();
						Console.WriteLine();
						i++;
					}
					Console.WriteLine("допомiжна таблиця");
					i=0;
					foreach(Entry2 item in table2.hashtable){
						Console.Write("["+i+"]");
						if(item.key!=null){
							Console.Write(item.key+":");
							foreach(string str in item.value){
								Console.Write(" "+str);
							}
							Console.WriteLine();
						}else if(item.DELETED)Console.WriteLine("[DELETED]");
						else Console.WriteLine();
						i++;
						Console.WriteLine();
					}
				}else if(mode=="6"){
					Console.WriteLine("введiть код гейту");
					string str=Console.ReadLine();
					int ID=table2.searchEntry(str);
					if(ID!=-1){
						for(int i=0; i<table2.hashtable[ID].value.Count; i++){
							Key key=new Key(table2.hashtable[ID].value[i]);
							Value item=table.findEntry(key).value;
							Console.WriteLine("код "+table2.hashtable[ID].value[i]+"\nгейт "+item.gate+"\nстарт "+item.departureTime.day+", "+item.departureTime.month+" "+item.departureTime.year+" "+item.departureTime.timeH+":"+item.departureTime.timeM+"\nвідкладено на "+item.isDelayedH+":"+item.isDelayedM);
						}
					}else Console.WriteLine("не iснує");
				}else if(mode=="7"){
					return;
				}else if(mode=="8"){
					Console.WriteLine("введiть нову кiлькiсть гейтiв");
					gateNumber=Convert.ToInt32(Console.ReadLine());
					table2.rehash(gateNumber);
				}else Console.WriteLine("помилка вводу");
			}
		}
	}
}