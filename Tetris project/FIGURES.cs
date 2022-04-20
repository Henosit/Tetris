using System.Drawing;

namespace FIGURES
{
	using System;
	using System.Collections;

    [Serializable] 
    public abstract class Figure
    {
        float x;
        float y;
        //Properties
        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public abstract void Draw(Graphics g);
        public abstract bool isInside(int xP, int yP);        
    }

    [Serializable] 
    public class myCircle: Figure 
	{
        float radius;  
		public myCircle() 
			: this(10,10,5)
        {}
		
		public myCircle(float xVal,float yVal,float rVal) 
		{
            X=xVal;
            Y=yVal;
            radius =rVal;
		}

        //Properties
        public float Radius 
		{
			get 
			{
				return radius;
			}
			set 
			{
				radius = value;
			}
		}

        public override void Draw(Graphics g)
        {
            SolidBrush br = new SolidBrush(Color.Red);
            Pen pen = new Pen(Color.Cyan, 2);
            g.FillEllipse(br, X - radius, Y - radius, 2 * radius, 2 * radius);
            g.DrawEllipse(pen, X - radius, Y - radius, 2 * radius, 2 * radius);
        }
        public override bool isInside(int xP, int yP)
        {
            return Math.Sqrt((xP - X) * (xP - X) + (yP - Y) * (yP - Y)) < radius;
        }

        ~myCircle() {}
	   
	}
    [Serializable]  
    public class myRectangle: Figure
    {
        float width;
        float height;
        public myRectangle()
            : this(10, 10, 10,20)
        { } 

        public myRectangle(float xVal, float yVal, float wVal, float hVal)
        {
            X = xVal;
            Y = yVal;
            width = wVal;
            height=hVal;
        }
        //Properties
        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public override void Draw(Graphics g)
        {
            SolidBrush br = new SolidBrush(Color.Red);
            Pen pen = new Pen(Color.Cyan, 2);
            g.FillRectangle(br, X - width/2, Y - height/2, width, height);
            g.DrawRectangle(pen, X - width/2, Y - height/2, width, height);
        }
        public override bool isInside(int xP, int yP)
        {
            return Math.Abs(xP - X)<=width/2 && Math.Abs(yP - Y) <= height / 2;
        }

        ~myRectangle() { }

    }

    [Serializable]
    public class mySquare : myRectangle
    {
        public mySquare()
            : this(10, 10, 10)
        { }

        public mySquare(float xVal, float yVal, float eVal)
        {
            X = xVal;
            Y = yVal;
            Width = eVal;
            Height = Width;
        }

        ~mySquare() { }
    }

    [Serializable]
    public class myTrapeze : myRectangle
    { 
        float topEdge;
        float bottomEdge;
        float sides;
        Point bottomLeft;
        public myTrapeze()
            : this(10, 10, 6,14,4,6,6)
        { }
    //   -------
    //  /_______\
    public myTrapeze(float xVal, float yVal, float topVal, float bottomVal, float heightVal, float BLxVal, float BLyVal)
        {
            X = xVal;
            Y = yVal;
            bottomLeft.X = (int)BLxVal;
            bottomLeft.Y = (int)BLyVal;
            topEdge=topVal;
        bottomEdge = bottomVal;
        Height = heightVal;
            sides = (float)Math.Sqrt(Math.Pow(Height, 2) + (Math.Pow(BLxVal - xVal, 2)));
        }
        //Properties

        public float TopEdge
        {
            get
            {
                return topEdge;
            }
            set
            {
            topEdge = value;
            }
        }

        public float BottomEdge
        {
            get
            {
                return bottomEdge;
            }
            set
            {
               bottomEdge = value;
            }
        }

        public float Sides
        {
            get 
            { 
                return sides; 
            }
            set 
            { 
                sides = value; 
            }  
        }

        public override void Draw(Graphics g)
        {
            SolidBrush br = new SolidBrush(Color.Red);
            Pen pen = new Pen(Color.Cyan, 2);
            //g.FillRectangle(br, X - width / 2, Y - height / 2, width, height);
            //g.DrawRectangle(pen, X - width / 2, Y - height / 2, width, height);
            //   ------- 
            //  /_______\
            Point p1 = new Point((int)X, (int)Y); 
            Point p2=new Point((int)(X +topEdge), (int)Y);
            Point p3 = new Point((int)(bottomLeft.X+BottomEdge), (int)(Y + Height));
            Point p4 = new Point(bottomLeft.X,bottomLeft.Y);
            Point [] trapezePoints = { p1, p2, p3, p4 };
            g.FillPolygon(br, trapezePoints);
            g.DrawPolygon(pen, trapezePoints);

        }
        public override bool isInside(int xP, int yP)
        {
            //return Math.Abs(xP - X) <= width / 2 && Math.Abs(yP - Y) <= height / 2;
        }
        ~myTrapeze() { }
    }


    [Serializable]  
    
    public class FigureList 
	{
		protected SortedList figures;
	
		public FigureList() 
		{
			figures = new SortedList();
		}
        //Properties
        public int NextIndex 
		{
			get 
			{
				return figures.Count;
			}
			
			
		}
		public Figure this[int index] 
		{
			get 
			{
				if (index >= figures.Count)
                    return (Figure)null;
				                       
                return (Figure)figures.GetByIndex(index);
			}
			set 
			{
				if ( index <= figures.Count )
					figures[index] = value; 		
			}
		}
        
        public void Remove(int element) 
		{
            if (element >= 0 && element < figures.Count)
            {
                for (int i = element; i < figures.Count - 1; i++)
                    figures[i] = figures[i + 1];
                figures.RemoveAt(figures.Count - 1);
            }
		}

        // new method
        public void DrawAll(Graphics g)
        {
            Figure prev, cur;
            for (int i = 1; i < figures.Count; i++)
            {
                prev = (Figure)figures[i-1];
                cur = (Figure)figures[i];
                g.DrawLine( Pens.Yellow, prev.X,prev.Y, cur.X, cur.Y );

                ((Figure)figures[i]).Draw(g);
            }
            for (int i = 0; i < figures.Count; i++)
                ((Figure)figures[i]).Draw(g);
        }
    
    }
}
