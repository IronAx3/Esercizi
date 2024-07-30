package it.cefi;

import javax.swing.JMenuBar;
import javax.swing.JMenu;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.KeyStroke;
import javax.swing.filechooser.FileNameExtensionFilter;

import java.awt.event.KeyEvent;
import java.io.File;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.StandardOpenOption;
import java.awt.event.InputEvent;
import javax.swing.JSeparator;
import javax.swing.JTextArea;
import javax.swing.ImageIcon;
import javax.swing.JFileChooser;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;

@SuppressWarnings("serial")
public class MyMenuBar extends JMenuBar {
	private JMenu mnFile;
	private JMenuItem mnOpen;
	private JMenuItem mnSave;
	private JSeparator separator;
	private JMenuItem mnExit;
	private JMenu mnHelp;
	private JTextArea area;
	
	public MyMenuBar(JTextArea area) {
		
		this.area=area;
		mnFile = new JMenu("File");
		mnFile.setMnemonic('F');
		add(mnFile);
		
		mnOpen = new JMenuItem("Open");
		mnOpen.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				try {
				String file= loadFile("Open");
				area.setText(Files.readString(Path.of(file)));
				
				} catch (Exception e1) {
					
					System.out.println(e1.getMessage());
				}
			}
		});
		mnOpen.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_O, InputEvent.CTRL_DOWN_MASK));
		mnFile.add(mnOpen);
		
		mnSave = new JMenuItem("Save");
		mnSave.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				try {
					String file=loadFile("Save");
					//Files.writeString(Path.of(file),area.getText(),StandardCharsets.UTF_8,StandardOpenOption.CREATE);
					//metodi che eseguono l'apertura e la chiusura del file
					//CREATE crea un nuovo file se non esiste
					//di default lavora come come CREATE ma se il file esiste lo sovrascrive
					Files.writeString(Path.of(file),area.getText(),StandardCharsets.UTF_8);
					area.setText("");
				} catch (Exception e1) {
					
				}
			}
		});
		mnSave.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_S, InputEvent.CTRL_DOWN_MASK));
		mnFile.add(mnSave);
		
		separator = new JSeparator();
		mnFile.add(separator);
		
		mnExit = new JMenuItem("Exit");
		mnExit.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				
			}
		});
		mnExit.setIcon(new ImageIcon("img\\exit.png"));
		mnExit.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_E, InputEvent.CTRL_DOWN_MASK));
		mnFile.add(mnExit);
		
		mnHelp = new JMenu("?");
		add(mnHelp);
	}
	//metodo che carica la finestra di esplora risorse e ritorna il file selezionato dall'utente
	private String loadFile(String op) throws Exception 
	{
		JFileChooser chooser=new JFileChooser();
		chooser.setFileFilter(new FileNameExtensionFilter("file txt", "txt","jpg"));
		int scelta=0;
		if(op.equals("Open"))
		{
			scelta=chooser.showOpenDialog(this);
		}
		else 
		{
			scelta=chooser.showSaveDialog(this);
		}
		if(scelta==JFileChooser.CANCEL_OPTION) 
		{
			throw new Exception("Hai annullato la selezione");
		}
		//oggetto della classe File che contiene il perscorso del file selezionato
		File file= chooser.getSelectedFile();
		
	    return file.getAbsolutePath();
	}

}
