using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class autoMapperTest
    {
        public autoMapperTest()
        {

            List<HotelProductEntity> list = new List<HotelProductEntity>();
            #region MyRegion
            long hotelID = 123L;
            string hotelCode = "hotelCode";
            string hotelName = "hotelName";
            string hotelNameEng = "hotelNameEng";
            int hotelState = 1;
            //房型
            int roomID = 100;
            string roomCode = "roomCode";
            string roomName = "roomName";
            string roomNameEng = "roomNameEng";
            int roomState = 1;
            // 售卖房型
            int sellRoomID = 222;
            string BedType = "BedType";
            int MaxNumber = 1;
            string BaseBoard = "BaseBoard";
            string sellRoomName = "sellRoomName";
            int slState = 1;
            // 每天售卖
            DateTime SellDate = DateTime.Now;
            decimal Amount = 0M;
            int pState = 1;
            DateTime ModifyTime = DateTime.Now;
            #endregion

            HotelProductEntity hotel = new HotelProductEntity();
            hotel.HotelID = hotelID;
            hotel.HotelCode = hotelCode; hotel.HotelNameCN = hotelName; hotel.HotelNameGB = hotelNameEng; hotel.IsValid = (hotelState == 1);
            hotel.SellStatusType = 1;
            hotel.Rooms = new List<RoomsEntity>();
            RoomsEntity room = new RoomsEntity();
            room.RoomID = roomID; room.RoomCode = roomCode; room.RoomNameCN = roomName; room.RoomNameGB = roomNameEng; room.IsValid = (roomState == 1);
            room.SellRooms = new List<SellRoomsEntity>();
            SellRoomsEntity sellRoom = new SellRoomsEntity();
            sellRoom.SellRoomID = sellRoomID; sellRoom.SellRoomNameCN = sellRoomName; sellRoom.SellRoomNameGB = ""; sellRoom.LodgersLimit = MaxNumber;
            sellRoom.BedType = BedType;
            sellRoom.MealType = BaseBoard;
            sellRoom.IsValid = (slState == 1);
            sellRoom.SellRoomCode = ""; sellRoom.CancelType = 1;
            sellRoom.Sells = new List<SellsEntity>();
            SellsEntity sell = new SellsEntity();
            sell.SellDate = SellDate.ToString("yyyy-MM-dd");
            sell.Price = Amount + 10; sell.IsValid = (pState == 1); sell.ModifyTime = ModifyTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            sellRoom.Sells.Add(sell);
            room.SellRooms.Add(sellRoom);
            hotel.Rooms.Add(room);
            list.Add(hotel);

            //MapperConfigurationExpression config = new MapperConfigurationExpression();
            //Mapper.Initialize(config);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<HotelProductEntity, HotelProduct>()
                  .ForMember(dest => dest._HotelID1, opt => opt.MapFrom(src => src.HotelID))
                  .ForMember(dest => dest._HotelCode1, opt => opt.MapFrom(src => src.HotelCode))
                  .ForMember(dest => dest._HotelNameCN1, opt => opt.MapFrom(src => src.HotelNameCN))
                  .ForMember(dest => dest._SellStatusType1, opt => opt.MapFrom(src => src.SellStatusType));
                cfg.CreateMap<SellsEntity, Sells>().ForMember(dest => dest._isValid1, opt => opt.MapFrom(src => src.IsValid))
                .ForMember(dest => dest._modifyTime1, opt => opt.MapFrom(src => src.ModifyTime))
                .ForMember(dest => dest._price1, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest._sellDate1, opt => opt.MapFrom(src => src.SellDate));
            }
    );

            List<HotelProduct> destination = Mapper.Map<List<HotelProductEntity>, List<HotelProduct>>(list);

            Sells sells = destination[0]._Rooms[0]._SellRooms[0]._Sells[0];
        }

    }
}
