using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest
{ 
    /// <summary>
    ///  
    /// </summary>
   public class HotelProductEntity
   { 
       public long HotelID { get; set; } 
       public string HotelCode { get; set; } 
       public string HotelNameCN { get; set; } 
       public string HotelNameGB { get; set; }  
       public int SellStatusType { get; set; }    
       public int  LatestBookDay { get; set; }   
       public bool IsValid { get; set; }  
       public List<RoomsEntity> Rooms { get; set; } 
    }

  public class RoomsEntity
   { 
       public int RoomID { get; set; } 
       public string RoomCode { get; set; } 
       public string RoomNameCN { get; set; } 
       public string RoomNameGB { get; set; } 
       public bool IsValid { get; set; } 
       public List<SellRoomsEntity> SellRooms { get; set; }  
   }
  public class SellRoomsEntity
   { 
       public int SellRoomID { get; set; } 
       public string SellRoomCode { get; set; } 
       public string SellRoomNameCN { get; set; } 
      public string SellRoomNameGB { get; set; } 
       public string BedType { get; set; } 
       public int LodgersLimit { get; set; } 
       public string MealType { get; set; }   
       public int CancelType { get; set; }  
       public string Remark { get; set; }  
       public bool IsValid { get; set; } 
       public List<SellsEntity> Sells { get; set; }  
   }
  public class SellsEntity
   { 
       public string SellDate { get; set; }  
       public Decimal Price { get; set; }  
       public bool IsValid { get; set; } 
       public string ModifyTime { get; set; } 
   }
}
