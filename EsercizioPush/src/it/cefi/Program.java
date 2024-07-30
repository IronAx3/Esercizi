package it.cefi;

import java.io.FileWriter;
import java.io.IOException;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Random;

public class Program {

	public static void main(String[] args) {
		
		List<Integer> numbers=new ArrayList<>();
		Random rnd=new Random();
		
		for(int i=0;i<10;++i)
		 numbers.add(rnd.nextInt(100));
	  
		String str=numbers.toString()+" " +loadDateTime()+System.lineSeparator();
			
		System.out.println(numbers);
		List<Integer> lista=Arrays.asList(10,20,30);
        numbers.addAll(0, lista);
       for(int i=0;i<lista.size();++i)
    	   numbers.remove(numbers.size()-1);
        str+=numbers.toString()+" " +loadDateTime()+System.lineSeparator();
        try(FileWriter out=new FileWriter("pushedout.txt"))
        {
        	out.write(str);
        } catch (IOException e) {}
	}
	public static String loadDateTime()
	{
	 return  LocalDateTime.now().format(DateTimeFormatter.ofPattern("dd/MM/yyyy HH:mm:ss:SS")); 
	}

}
