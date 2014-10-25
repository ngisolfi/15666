using UnityEngine;
using System.Collections;

public class overlay_ss : MonoBehaviour {
	
	
	
	public GameObject gridobject;
	public GameObject planobject;

	//private make_grid grid;
	private Make_grid_raycast grid;
	
	private Color linecolor = new Color (0f, 1f, 0f, 1f);
	private Color cellcolor = new Color (1f, 0f, 0f, 1f);
	private Color pathcolor = new Color (0f, 0f, 1f, 1f);
	private Color opencolor = new Color (1f, 1f, 0f, 1f);
	private Color closedcolor = new Color (0f, 1f, 1f, 1f);
	private Material linematerial;
	private Material cellmaterial;
	
	
	// Use this for initialization
	void Start () {
		
		grid = gridobject.GetComponent<Make_grid_raycast> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void CreateLineMaterial() 
	{
		
		if( !linematerial ) {
			linematerial = new Material( "Shader \"Lines/Colored Blended\" {" +
			                            "SubShader { Pass { " +
			                            "    Blend SrcAlpha OneMinusSrcAlpha " +
			                            "    ZWrite Off Cull Off Fog { Mode Off } " +
			                            "    BindChannels {" +
			                            "      Bind \"vertex\", vertex Bind \"color\", color }" +
			                            "} } }" );
			linematerial.hideFlags = HideFlags.HideAndDontSave;
			linematerial.shader.hideFlags = HideFlags.HideAndDontSave;}
	}
	
	void CreateCellMaterial() 
	{
		
		if( !cellmaterial ) {
			cellmaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
			                            "SubShader { Pass { " +
			                            "    Blend SrcAlpha OneMinusSrcAlpha " +
			                            "    ZWrite Off Cull Off Fog { Mode Off } " +
			                            "    BindChannels {" +
			                            "      Bind \"vertex\", vertex Bind \"color\", color }" +
			                            "} } }" );
			cellmaterial.hideFlags = HideFlags.HideAndDontSave;
			cellmaterial.shader.hideFlags = HideFlags.HideAndDontSave;}
	}
	
	void OnPostRender(){
		CreateLineMaterial ();
		
		
		linematerial.SetPass (0);
		
		GL.Begin (GL.LINES);
		GL.Color (linecolor);
		
		//Z axis lines
		for (float i = 0; i <= ((grid.endx-grid.startx)/grid.cellsize); i += grid.cellsize) {
			GL.Vertex3 (grid.startx + i, 0.1f, grid.startz);
			GL.Vertex3 (grid.startx + i, 0.1f, grid.endz);
		}
		
		//X axis lines
		for (float i = 0; i <= ((grid.endz-grid.startz)/grid.cellsize); i += grid.cellsize) {
			GL.Vertex3 (grid.startx, 0.1f, grid.startz + i);
			GL.Vertex3 (grid.endx, 0.1f, grid.startz + i);
		}
		
		GL.End ();
		//
		CreateCellMaterial ();
		cellmaterial.SetPass (0);
		//
		GL.Color (cellcolor);
		GL.Begin (GL.QUADS);
		for (int i = 0; i<(int)((grid.endx-grid.startx)/grid.cellsize); i++ ){
			for (int j=0; j<(int)((grid.endz-grid.startz)/grid.cellsize); j++) {
				if (grid.grid [i, j] == 1) {
					GL.Vertex3 (grid.startx+i * grid.cellsize, 0.1f, grid.startz+j * grid.cellsize);
					GL.Vertex3 (grid.startx+i * grid.cellsize, 0.1f, grid.startz+(j + 1) * grid.cellsize);
					GL.Vertex3 (grid.startx+(i + 1) * grid.cellsize, 0.1f, grid.startz+(j + 1) * grid.cellsize);
					GL.Vertex3 (grid.startx+(i + 1) * grid.cellsize, 0.1f, grid.startz+j * grid.cellsize);
				} else {
					//print green
					//cellMaterial.SetPass (0);
					//					GL.Color (cell1Color);
					//GL.Begin (GL.QUADS);
					//					GL.Vertex3 (startx+i * cellSize, 0.0f, startz+j * cellSize);
					//					GL.Vertex3 (startx+i * cellSize, 0.0f, startz+(j + 1) * cellSize);
					//					GL.Vertex3 (startx+(i + 1) * cellSize, 0.0f, startz+(j + 1) * cellSize);
					//					GL.Vertex3 (startx+(i + 1) * cellSize, 0.0f, startz+j * cellSize);
					
					
					//GL.End ();
				}
			}
		}
		GL.End ();
		
		
		
		
		
		//open cells
		GL.Color (opencolor);
		GL.Begin (GL.QUADS);
		for (int i = 0; i<(int)((grid.endx-grid.startx)/grid.cellsize); i++ ){
			for (int j=0; j<(int)((grid.endz-grid.startz)/grid.cellsize); j++) {
				if (grid.oMap [i, j] == 1) {
					GL.Vertex3 (grid.startx+i * grid.cellsize, 0.1f, grid.startz+j * grid.cellsize);
					GL.Vertex3 (grid.startx+i * grid.cellsize, 0.1f, grid.startz+(j + 1) * grid.cellsize);
					GL.Vertex3 (grid.startx+(i + 1) * grid.cellsize, 0.1f, grid.startz+(j + 1) * grid.cellsize);
					GL.Vertex3 (grid.startx+(i + 1) * grid.cellsize, 0.1f, grid.startz+j * grid.cellsize);
					grid.oMap[i,j]=0;
				} else {
					//print green
					//cellMaterial.SetPass (0);
					//					GL.Color (cell1Color);
					//GL.Begin (GL.QUADS);
					//					GL.Vertex3 (startx+i * cellSize, 0.0f, startz+j * cellSize);
					//					GL.Vertex3 (startx+i * cellSize, 0.0f, startz+(j + 1) * cellSize);
					//					GL.Vertex3 (startx+(i + 1) * cellSize, 0.0f, startz+(j + 1) * cellSize);
					//					GL.Vertex3 (startx+(i + 1) * cellSize, 0.0f, startz+j * cellSize);
					
					
					//GL.End ();
				}
			}
		}
		GL.End ();
		
		
		
		//open cells
		GL.Color (closedcolor);
		GL.Begin (GL.QUADS);
		for (int i = 0; i<(int)((grid.endx-grid.startx)/grid.cellsize); i++ ){
			for (int j=0; j<(int)((grid.endz-grid.startz)/grid.cellsize); j++) {
				if (grid.cMap [i, j] == 1) {
					GL.Vertex3 (grid.startx+i * grid.cellsize, 0.1f, grid.startz+j * grid.cellsize);
					GL.Vertex3 (grid.startx+i * grid.cellsize, 0.1f, grid.startz+(j + 1) * grid.cellsize);
					GL.Vertex3 (grid.startx+(i + 1) * grid.cellsize, 0.1f, grid.startz+(j + 1) * grid.cellsize);
					GL.Vertex3 (grid.startx+(i + 1) * grid.cellsize, 0.1f, grid.startz+j * grid.cellsize);
					grid.cMap[i,j]=0;
				} else {
					//print green
					//cellMaterial.SetPass (0);
					//					GL.Color (cell1Color);
					//GL.Begin (GL.QUADS);
					//					GL.Vertex3 (startx+i * cellSize, 0.0f, startz+j * cellSize);
					//					GL.Vertex3 (startx+i * cellSize, 0.0f, startz+(j + 1) * cellSize);
					//					GL.Vertex3 (startx+(i + 1) * cellSize, 0.0f, startz+(j + 1) * cellSize);
					//					GL.Vertex3 (startx+(i + 1) * cellSize, 0.0f, startz+j * cellSize);
					
					
					//GL.End ();
				}
			}
		}
		GL.End ();
		
		
		//path cells
		GL.Color (pathcolor);
		GL.Begin (GL.QUADS);
		for (int i = 0; i<(int)((grid.endx-grid.startx)/grid.cellsize); i++ ){
			for (int j=0; j<(int)((grid.endz-grid.startz)/grid.cellsize); j++) {
				if (grid.pMap [i, j] == 1) {
					GL.Vertex3 (grid.startx+i * grid.cellsize, 0.1f, grid.startz+j * grid.cellsize);
					GL.Vertex3 (grid.startx+i * grid.cellsize, 0.1f, grid.startz+(j + 1) * grid.cellsize);
					GL.Vertex3 (grid.startx+(i + 1) * grid.cellsize, 0.1f, grid.startz+(j + 1) * grid.cellsize);
					GL.Vertex3 (grid.startx+(i + 1) * grid.cellsize, 0.1f, grid.startz+j * grid.cellsize);
					grid.pMap[i,j]=0;
				} else {
					//print green
					//cellMaterial.SetPass (0);
					//					GL.Color (cell1Color);
					//GL.Begin (GL.QUADS);
					//					GL.Vertex3 (startx+i * cellSize, 0.0f, startz+j * cellSize);
					//					GL.Vertex3 (startx+i * cellSize, 0.0f, startz+(j + 1) * cellSize);
					//					GL.Vertex3 (startx+(i + 1) * cellSize, 0.0f, startz+(j + 1) * cellSize);
					//					GL.Vertex3 (startx+(i + 1) * cellSize, 0.0f, startz+j * cellSize);
					
					
					//GL.End ();
				}
			}
		}
		GL.End ();
		
	}
}
