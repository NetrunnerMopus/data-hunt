using System.Threading.Tasks;

public delegate Task AsyncAction<Event>(Event e);
