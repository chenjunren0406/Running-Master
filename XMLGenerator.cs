using UnityEngine;
using System.IO;
using System.Xml;
using System.Collections;

public class XMLGenerator : MonoBehaviour {
	private string _xmlpath;
	private XmlDocument doc;
	private GameObject ground;
	private GameObject wall;
	public string pathname;
	private Quaternion rotation;

	void Start(){
		_xmlpath = Application.dataPath + "/MazeXML/" + pathname + ".xml";


		doc = new XmlDocument ();
		doc.Load (_xmlpath);
		readXMLAndGenerate ();

	}
	
	void Update(){

	}

	void readXMLAndGenerate(){
		XmlNodeList groundData = doc.SelectSingleNode("maze").SelectNodes("g");
		XmlNodeList wallData = doc.SelectSingleNode ("maze").SelectNodes("w");
		GenerateGround (groundData);
		GenerateWall(wallData);
	}
	
	void GenerateGround(XmlNodeList groundData){
		/*
		 * this is to generate ground
		 */
		foreach(XmlNode g in groundData){

			ground = Resources.Load("Plane") as GameObject;

			foreach(XmlElement _g in g){
				float x1 = float.Parse(_g.GetAttribute("x1"));
				float x2 = float.Parse(_g.GetAttribute("x2"));
				float y1 = float.Parse(_g.GetAttribute("y1"));
				float y2 = float.Parse(_g.GetAttribute("y2"));
				
				if(y1 == y2){
					
					//make sure that x1 is start point, which is smaller than x2
					if(x1 > x2){
						float tmp = x1;
						x1 = x2;
						x2 = tmp;
					}
					
					// begin to instaniate
					for(;x1<=x2;x1++){

						if(y1%2 == 0){
							rotation = Quaternion.Euler(new Vector3(0,0,0));
						}
						else 
						{
							rotation = Quaternion.Euler(new Vector3(0,180,0));
						}
						
						Instantiate(ground,new Vector3(x1*100,0,y1*100),rotation);
						
					}
					
				}
				else if(x1 == x2){
					if(y1 > y2){
						float tmp = y1;
						y1 = y2;
						y2 = tmp;
					}
					
					for(;y1<=y2;y1++){
						Quaternion rotation;
						if(y1%2 == 0){
							rotation = Quaternion.Euler(new Vector3(0,0,0));
						}
						else 
						{
							rotation = Quaternion.Euler(new Vector3(0,180,0));
						}
						Instantiate(ground,new Vector3(x1*100,0,y1*100),rotation);
						
					}
				}
			}
		}
	}

	void GenerateWall(XmlNodeList wallData){
		foreach(XmlNode w in wallData){

			wall = Resources.Load ("LeftWall") as GameObject;

			foreach(XmlElement _w in w){
				float x1 = float.Parse(_w.GetAttribute("x1"));
				float x2 = float.Parse(_w.GetAttribute("x2"));
				float y1 = float.Parse(_w.GetAttribute("y1"));
				float y2 = float.Parse(_w.GetAttribute("y2"));
				string direction = _w.GetAttribute("direction");
				Debug.Log(direction);

				float offsetx = 0f;
				float offsety = 0f;

		
				switch(direction){
				case "Up":
					rotation = Quaternion.Euler(new Vector3(0,90,0));
					offsety = 50f;
					offsetx = 0f;
					break;
					
				case "Down":
					rotation = Quaternion.Euler(new Vector3(0,270,0));
					offsety = -50f;
					offsetx = 0f;
					break;
					
				case "Left":
					rotation = Quaternion.Euler(new Vector3(0,0,0));
					offsetx = -50f;
					offsety = 0f;
					break;
					
				case "Right":
					rotation = Quaternion.Euler(new Vector3(0,180,0));
					offsetx = 50f;
					offsety = 0f;
					break;
					
					
				}

				if(x1 == x2){
					if(y1 > y2){
						float tmp = y1;
						y1 = y2;
						y2 = tmp;
					}
					for(; y1 <= y2 ; y1++){

						Instantiate(wall,new Vector3(x1*100+offsetx,50,y1*100+offsety),rotation);
					}
				}

				else if(y1 == y2){
					if(x1 > x2){
						float tmp = x1;
						x1 = x2;
						x2 = tmp;
					}
					for(;x1<=x2;x1++){
						Instantiate(wall,new Vector3(x1 * 100 + offsetx,50,y1 * 100 + offsety),rotation);
					}
				}
			}
		}
	}

}
