using System.Collections.Generic;

public class DungeonGenerator 
{
    RoomNode roomNode;
    List<RoomNode> allSpaceNodes = new List<RoomNode>();

    private int dungeonWidth;
    private int dungeonLength;

    public DungeonGenerator(int dungeonWidth, int dungeonHeight)
    {
        this.dungeonWidth = dungeonWidth;
        this.dungeonLength = dungeonHeight;
    }

    public List<Node> CalculateRooms(int maxIterations, int roomWidthMin, int roomLengthMin,
        float roomBottomCornerModifier, float roomTopCornerModifier, int roomOffset)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(dungeonWidth, dungeonLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
        List<Node> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeafes(bsp.RootNode);

        RoomGenerator roomGenerator = new RoomGenerator(maxIterations, roomLengthMin, roomWidthMin);
        List<RoomNode> roomList = roomGenerator.GenerateRoomsInGivenSpaces(
            roomSpaces, 
            roomBottomCornerModifier, 
            roomTopCornerModifier, 
            roomOffset);

        return new List<Node>(roomList);
    }
}
