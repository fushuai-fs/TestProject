using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest
{ 
    /// <summary>
    ///  
    /// </summary>
   public class HotelProduct
   { 
       public long _HotelID1 { get; set; } 
       public string _HotelCode1 { get; set; } 
       public string _HotelNameCN1 { get; set; } 
       public string _HotelNameGB1 { get; set; }  
       public int _SellStatusType1 { get; set; }    
       public int _LatestBookDay { get; set; }   
       public bool _IsValid { get; set; }  
       public List<Rooms> _Rooms { get; set; } 
    }

  public class Rooms
   { 
       public int _RoomID { get; set; } 
       public string _RoomCode { get; set; } 
       public string _RoomNameCN { get; set; } 
       public string _RoomNameGB { get; set; } 
       public bool _IsValid { get; set; } 
       public List<SellRooms> _SellRooms { get; set; }  
   }
  public class SellRooms
   { 
       public int _SellRoomID { get; set; } 
       public string _SellRoomCode { get; set; } 
       public string _SellRoomNameCN { get; set; } 
      public string _SellRoomNameGB { get; set; } 
       public string _BedType { get; set; } 
       public int _LodgersLimit { get; set; } 
       public string _MealType { get; set; }   
       public int _CancelType { get; set; }  
       public string _Remark { get; set; }  
       public bool _IsValid { get; set; } 
       public List<Sells> _Sells { get; set; }  
   }
  public class Sells
   { 
       public string _sellDate1 { get; set; }  
       public Decimal _price1 { get; set; }  
       public bool _isValid1 { get; set; } 
       public string _modifyTime1 { get; set; } 
   }
}
