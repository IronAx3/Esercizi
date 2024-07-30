package it.cefi;

import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;
import java.awt.BorderLayout;
import javax.swing.JLabel;
import javax.swing.SwingConstants;
import java.awt.Font;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.Timer;
import java.util.TimerTask;

public class MyFrame extends JFrame {

	private static final long serialVersionUID = 1L;
	private JPanel contentPane;
	private JLabel lblOrologio;

	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					MyFrame frame = new MyFrame();
					frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the frame.
	 */
	public MyFrame() {
		setTitle("Orologio");
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(100, 100, 450, 300);
		contentPane = new JPanel();
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));

		setContentPane(contentPane);
		contentPane.setLayout(new BorderLayout(0, 0));
		
		lblOrologio = new JLabel("");
		lblOrologio.setFont(new Font("Tahoma", Font.BOLD, 20));
		lblOrologio.setHorizontalAlignment(SwingConstants.CENTER);
		contentPane.add(lblOrologio, BorderLayout.CENTER);
		loadTimer();
		
		Timer timer=new Timer();//prendi il metodo di java.util
		//timer usando la classe annidata
		timer.schedule(new GestioneTimer(),0,1000);
		
		//timer usando la classe anonima che eredita la classe astratta TimerTask
		/*
		 timer schedule:
		 1 parametro:
		 2 parametro:quando deve partire, se imposti 0 parte subito
		 3 parametro:millisecondi(imposta 1000)
		 */
		
		/*timer.schedule(new TimerTask() {
			
			@Override
			public void run() {
				loadTimer();
				
			}
		}, 0, 1000);
	     */
	}
	
	private void loadTimer() 
	{
		LocalDateTime data= LocalDateTime.now();
		//classe in cui viene memorizzato la formattazione da applicare successivamente
		DateTimeFormatter dateFormat= DateTimeFormatter.ofPattern("HH:mm:ss");//ore minuti secondi
		//il metodo format ritorna una stringa in base alla formattazione impostata nel DateTimeFormatter
		lblOrologio.setText(data.format(dateFormat));
		//lblOrologio.setText(data+"");
	}

    private	class GestioneTimer extends TimerTask{

		@Override
		public void run() {
			loadTimer();
			
		}		
	}
	


}
