public class GraphEdge
{
   public GraphNode from, to;
   public float travelCost;

   public GraphEdge(GraphNode from, GraphNode to, float travelCost){
       this.from = from;
       this.to = to;
       this.travelCost = travelCost;
   }
}
