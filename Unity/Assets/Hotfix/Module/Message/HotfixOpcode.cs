using ETModel;
namespace ETHotfix
{
	[Message(HotfixOpcode.C2R_Login)]
	public partial class C2R_Login : IRequest {}

	[Message(HotfixOpcode.R2C_Login)]
	public partial class R2C_Login : IResponse {}

	[Message(HotfixOpcode.C2G_LoginGate)]
	public partial class C2G_LoginGate : IRequest {}

	[Message(HotfixOpcode.G2C_LoginGate)]
	public partial class G2C_LoginGate : IResponse {}

	[Message(HotfixOpcode.G2C_TestHotfixMessage)]
	public partial class G2C_TestHotfixMessage : IMessage {}

	[Message(HotfixOpcode.C2M_TestActorRequest)]
	public partial class C2M_TestActorRequest : IActorLocationRequest {}

	[Message(HotfixOpcode.M2C_TestActorResponse)]
	public partial class M2C_TestActorResponse : IActorLocationResponse {}

	[Message(HotfixOpcode.PlayerInfo)]
	public partial class PlayerInfo : IMessage {}

	[Message(HotfixOpcode.C2G_PlayerInfo)]
	public partial class C2G_PlayerInfo : IRequest {}

	[Message(HotfixOpcode.G2C_PlayerInfo)]
	public partial class G2C_PlayerInfo : IResponse {}

	[Message(HotfixOpcode.C2G_AddAccountInfo)]
	public partial class C2G_AddAccountInfo : IRequest {}

	[Message(HotfixOpcode.G2C_AddAccountInfo)]
	public partial class G2C_AddAccountInfo : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateAccountInfo)]
	public partial class C2G_UpdateAccountInfo : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateAccountInfo)]
	public partial class G2C_UpdateAccountInfo : IResponse {}

	[Message(HotfixOpcode.C2G_QueryAccountInfo)]
	public partial class C2G_QueryAccountInfo : IRequest {}

	[Message(HotfixOpcode.G2C_QueryAccountInfo)]
	public partial class G2C_QueryAccountInfo : IResponse {}

	[Message(HotfixOpcode.C2G_AddGuestAccount)]
	public partial class C2G_AddGuestAccount : IRequest {}

	[Message(HotfixOpcode.G2C_AddGuestAccount)]
	public partial class G2C_AddGuestAccount : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateGuestAccount)]
	public partial class C2G_UpdateGuestAccount : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateGuestAccount)]
	public partial class G2C_UpdateGuestAccount : IResponse {}

	[Message(HotfixOpcode.C2G_QueryGuestAccount)]
	public partial class C2G_QueryGuestAccount : IRequest {}

	[Message(HotfixOpcode.G2C_QueryGuestAccount)]
	public partial class G2C_QueryGuestAccount : IResponse {}

	[Message(HotfixOpcode.C2G_GuestToMainAccount)]
	public partial class C2G_GuestToMainAccount : IRequest {}

	[Message(HotfixOpcode.G2C_GuestToMainAccount)]
	public partial class G2C_GuestToMainAccount : IResponse {}

	[Message(HotfixOpcode.C2G_AddMainAccount)]
	public partial class C2G_AddMainAccount : IRequest {}

	[Message(HotfixOpcode.G2C_AddMainAccount)]
	public partial class G2C_AddMainAccount : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateMainAccount)]
	public partial class C2G_UpdateMainAccount : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateMainAccount)]
	public partial class G2C_UpdateMainAccount : IResponse {}

	[Message(HotfixOpcode.C2G_QueryMainAccount)]
	public partial class C2G_QueryMainAccount : IRequest {}

	[Message(HotfixOpcode.G2C_QueryMainAccount)]
	public partial class G2C_QueryMainAccount : IResponse {}

	[Message(HotfixOpcode.C2G_UserAdress)]
	public partial class C2G_UserAdress : IRequest {}

	[Message(HotfixOpcode.G2C_UserAdress)]
	public partial class G2C_UserAdress : IResponse {}

	[Message(HotfixOpcode.C2G_QueryUserAdress)]
	public partial class C2G_QueryUserAdress : IRequest {}

	[Message(HotfixOpcode.G2C_QueryUserAdress)]
	public partial class G2C_QueryUserAdress : IResponse {}

	[Message(HotfixOpcode.C2G_AddUserLoginRecord)]
	public partial class C2G_AddUserLoginRecord : IRequest {}

	[Message(HotfixOpcode.G2C_AddUserLoginRecord)]
	public partial class G2C_AddUserLoginRecord : IResponse {}

	[Message(HotfixOpcode.C2G_QueryUserLoginRecord)]
	public partial class C2G_QueryUserLoginRecord : IRequest {}

	[Message(HotfixOpcode.G2C_QueryUserLoginRecord)]
	public partial class G2C_QueryUserLoginRecord : IResponse {}

	[Message(HotfixOpcode.LoginRecord)]
	public partial class LoginRecord : IMessage {}

	[Message(HotfixOpcode.C2G_AddUserPortrait)]
	public partial class C2G_AddUserPortrait : IRequest {}

	[Message(HotfixOpcode.G2C_AddUserPortrait)]
	public partial class G2C_AddUserPortrait : IResponse {}

	[Message(HotfixOpcode.C2G_QueryUserPortrait)]
	public partial class C2G_QueryUserPortrait : IRequest {}

	[Message(HotfixOpcode.G2C_QueryUserPortrait)]
	public partial class G2C_QueryUserPortrait : IResponse {}

	[Message(HotfixOpcode.C2G_UserProductInfo)]
	public partial class C2G_UserProductInfo : IRequest {}

	[Message(HotfixOpcode.G2C_UserProductInfo)]
	public partial class G2C_UserProductInfo : IResponse {}

	[Message(HotfixOpcode.C2G_QueryUserProductInfo)]
	public partial class C2G_QueryUserProductInfo : IRequest {}

	[Message(HotfixOpcode.G2C_QueryUserProductInfo)]
	public partial class G2C_QueryUserProductInfo : IResponse {}

	[Message(HotfixOpcode.C2G_AddUserProductRecord)]
	public partial class C2G_AddUserProductRecord : IRequest {}

	[Message(HotfixOpcode.G2C_AddUserProductRecord)]
	public partial class G2C_AddUserProductRecord : IResponse {}

	[Message(HotfixOpcode.C2G_QueryUserProductRecord)]
	public partial class C2G_QueryUserProductRecord : IRequest {}

	[Message(HotfixOpcode.G2C_QueryUserProductRecord)]
	public partial class G2C_QueryUserProductRecord : IResponse {}

	[Message(HotfixOpcode.ProductRecord)]
	public partial class ProductRecord : IMessage {}

	[Message(HotfixOpcode.C2G_AddUserRequestRecord)]
	public partial class C2G_AddUserRequestRecord : IRequest {}

	[Message(HotfixOpcode.G2C_AddUserRequestRecord)]
	public partial class G2C_AddUserRequestRecord : IResponse {}

	[Message(HotfixOpcode.C2G_QueryUserRequestRecord)]
	public partial class C2G_QueryUserRequestRecord : IRequest {}

	[Message(HotfixOpcode.G2C_QueryUserRequestRecord)]
	public partial class G2C_QueryUserRequestRecord : IResponse {}

	[Message(HotfixOpcode.RequestRecord)]
	public partial class RequestRecord : IMessage {}

	[Message(HotfixOpcode.C2G_AddDealOrder)]
	public partial class C2G_AddDealOrder : IRequest {}

	[Message(HotfixOpcode.G2C_AddDealOrder)]
	public partial class G2C_AddDealOrder : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateDealOrder)]
	public partial class C2G_UpdateDealOrder : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateDealOrder)]
	public partial class G2C_UpdateDealOrder : IResponse {}

	[Message(HotfixOpcode.C2G_QueryDealOrder)]
	public partial class C2G_QueryDealOrder : IRequest {}

	[Message(HotfixOpcode.G2C_QueryDealOrder)]
	public partial class G2C_QueryDealOrder : IResponse {}

	[Message(HotfixOpcode.Dealorder)]
	public partial class Dealorder : IMessage {}

	[Message(HotfixOpcode.C2G_AddGoodsOrder)]
	public partial class C2G_AddGoodsOrder : IRequest {}

	[Message(HotfixOpcode.G2C_AddGoodsOrder)]
	public partial class G2C_AddGoodsOrder : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateGoodsOrder)]
	public partial class C2G_UpdateGoodsOrder : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateGoodsOrder)]
	public partial class G2C_UpdateGoodsOrder : IResponse {}

	[Message(HotfixOpcode.C2G_QueryGoodsOrder)]
	public partial class C2G_QueryGoodsOrder : IRequest {}

	[Message(HotfixOpcode.G2C_QueryGoodsOrder)]
	public partial class G2C_QueryGoodsOrder : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateChatRoomMessage)]
	public partial class C2G_UpdateChatRoomMessage : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateChatRoomMessage)]
	public partial class G2C_UpdateChatRoomMessage : IResponse {}

	[Message(HotfixOpcode.C2G_AddFriendInfo)]
	public partial class C2G_AddFriendInfo : IRequest {}

	[Message(HotfixOpcode.G2C_AddFriendInfo)]
	public partial class G2C_AddFriendInfo : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateFriendInfo)]
	public partial class C2G_UpdateFriendInfo : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateFriendInfo)]
	public partial class G2C_UpdateFriendInfo : IResponse {}

	[Message(HotfixOpcode.C2G_QueryFriendInfo)]
	public partial class C2G_QueryFriendInfo : IRequest {}

	[Message(HotfixOpcode.G2C_QueryFriendInfo)]
	public partial class G2C_QueryFriendInfo : IResponse {}

	[Message(HotfixOpcode.C2G_AddGroupInfo)]
	public partial class C2G_AddGroupInfo : IRequest {}

	[Message(HotfixOpcode.G2C_AddGroupInfo)]
	public partial class G2C_AddGroupInfo : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateGroupInfo)]
	public partial class C2G_UpdateGroupInfo : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateGroupInfo)]
	public partial class G2C_UpdateGroupInfo : IResponse {}

	[Message(HotfixOpcode.C2G_QueryGroupInfo)]
	public partial class C2G_QueryGroupInfo : IRequest {}

	[Message(HotfixOpcode.G2C_QueryGroupInfo)]
	public partial class G2C_QueryGroupInfo : IResponse {}

	[Message(HotfixOpcode.C2G_AddRelationInfo)]
	public partial class C2G_AddRelationInfo : IRequest {}

	[Message(HotfixOpcode.G2C_AddRelationInfo)]
	public partial class G2C_AddRelationInfo : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateRelationInfo)]
	public partial class C2G_UpdateRelationInfo : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateRelationInfo)]
	public partial class G2C_UpdateRelationInfo : IResponse {}

	[Message(HotfixOpcode.C2G_QueryRelationInfo)]
	public partial class C2G_QueryRelationInfo : IRequest {}

	[Message(HotfixOpcode.G2C_QueryRelationInfo)]
	public partial class G2C_QueryRelationInfo : IResponse {}

	[Message(HotfixOpcode.C2G_AddRequestInfo)]
	public partial class C2G_AddRequestInfo : IRequest {}

	[Message(HotfixOpcode.G2C_AddRequestInfo)]
	public partial class G2C_AddRequestInfo : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateRequestInfo)]
	public partial class C2G_UpdateRequestInfo : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateRequestInfo)]
	public partial class G2C_UpdateRequestInfo : IResponse {}

	[Message(HotfixOpcode.C2G_QueryRequestInfo)]
	public partial class C2G_QueryRequestInfo : IRequest {}

	[Message(HotfixOpcode.G2C_QueryRequestInfo)]
	public partial class G2C_QueryRequestInfo : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateUserMessage)]
	public partial class C2G_UpdateUserMessage : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateUserMessage)]
	public partial class G2C_UpdateUserMessage : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateWalletRecord)]
	public partial class C2G_UpdateWalletRecord : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateWalletRecord)]
	public partial class G2C_UpdateWalletRecord : IResponse {}

	[Message(HotfixOpcode.C2R_RegistForSaveAccount)]
	public partial class C2R_RegistForSaveAccount : IRequest {}

	[Message(HotfixOpcode.R2C_RegistForSaveAccount)]
	public partial class R2C_RegistForSaveAccount : IResponse {}

	[Message(HotfixOpcode.C2R_FindPassword)]
	public partial class C2R_FindPassword : IRequest {}

	[Message(HotfixOpcode.R2C_FindPassword)]
	public partial class R2C_FindPassword : IResponse {}

	[Message(HotfixOpcode.C2R_CompareAccount)]
	public partial class C2R_CompareAccount : IRequest {}

	[Message(HotfixOpcode.R2C_CompareAccount)]
	public partial class R2C_CompareAccount : IResponse {}

	[Message(HotfixOpcode.C2R_ForSaveInformation)]
	public partial class C2R_ForSaveInformation : IRequest {}

	[Message(HotfixOpcode.R2C_ForSaveInformation)]
	public partial class R2C_ForSaveInformation : IResponse {}

	[Message(HotfixOpcode.C2G_ChangePassword)]
	public partial class C2G_ChangePassword : IRequest {}

	[Message(HotfixOpcode.G2C_ChangePassword)]
	public partial class G2C_ChangePassword : IResponse {}

	[Message(HotfixOpcode.C2G_GetPersonalInformation)]
	public partial class C2G_GetPersonalInformation : IRequest {}

	[Message(HotfixOpcode.G2C_GetPersonalInformation)]
	public partial class G2C_GetPersonalInformation : IResponse {}

	[Message(HotfixOpcode.C2G_AddressManager)]
	public partial class C2G_AddressManager : IRequest {}

	[Message(HotfixOpcode.G2C_AddressManager)]
	public partial class G2C_AddressManager : IResponse {}

	[Message(HotfixOpcode.C2G_LoginRecord)]
	public partial class C2G_LoginRecord : IRequest {}

	[Message(HotfixOpcode.G2C_LoginRecord)]
	public partial class G2C_LoginRecord : IResponse {}

	[Message(HotfixOpcode.C2G_FriendList)]
	public partial class C2G_FriendList : IRequest {}

	[Message(HotfixOpcode.G2C_FriendList)]
	public partial class G2C_FriendList : IResponse {}

	[Message(HotfixOpcode.C2G_AddFriendID)]
	public partial class C2G_AddFriendID : IRequest {}

	[Message(HotfixOpcode.G2C_AddFriendID)]
	public partial class G2C_AddFriendID : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateFriendID)]
	public partial class C2G_UpdateFriendID : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateFriendID)]
	public partial class G2C_UpdateFriendID : IResponse {}

	[Message(HotfixOpcode.C2G_FriendInfoDateMessage)]
	public partial class C2G_FriendInfoDateMessage : IRequest {}

	[Message(HotfixOpcode.G2C_FriendInfoDateMessage)]
	public partial class G2C_FriendInfoDateMessage : IResponse {}

	[Message(HotfixOpcode.C2G_AddBlackID)]
	public partial class C2G_AddBlackID : IRequest {}

	[Message(HotfixOpcode.G2C_AddBlackID)]
	public partial class G2C_AddBlackID : IResponse {}

	[Message(HotfixOpcode.C2G_AddGroup)]
	public partial class C2G_AddGroup : IRequest {}

	[Message(HotfixOpcode.G2C_AddGroup)]
	public partial class G2C_AddGroup : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateGroup)]
	public partial class C2G_UpdateGroup : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateGroup)]
	public partial class G2C_UpdateGroup : IResponse {}

	[Message(HotfixOpcode.C2G_AddChatRoom)]
	public partial class C2G_AddChatRoom : IRequest {}

	[Message(HotfixOpcode.G2C_AddChatRoom)]
	public partial class G2C_AddChatRoom : IResponse {}

	[Message(HotfixOpcode.C2G_DeleteChatRoom)]
	public partial class C2G_DeleteChatRoom : IRequest {}

	[Message(HotfixOpcode.G2C_DeleteChatRoom)]
	public partial class G2C_DeleteChatRoom : IResponse {}

	[Message(HotfixOpcode.C2G_JoinChatRoom)]
	public partial class C2G_JoinChatRoom : IRequest {}

	[Message(HotfixOpcode.G2C_JoinChatRoom)]
	public partial class G2C_JoinChatRoom : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateChatRoom)]
	public partial class C2G_UpdateChatRoom : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateChatRoom)]
	public partial class G2C_UpdateChatRoom : IResponse {}

	[Message(HotfixOpcode.C2G_QueryChatRoom)]
	public partial class C2G_QueryChatRoom : IRequest {}

	[Message(HotfixOpcode.G2C_QueryChatRoom)]
	public partial class G2C_QueryChatRoom : IResponse {}

	[Message(HotfixOpcode.C2G_FriendApply)]
	public partial class C2G_FriendApply : IRequest {}

	[Message(HotfixOpcode.G2C_FriendApply)]
	public partial class G2C_FriendApply : IResponse {}

	[Message(HotfixOpcode.C2G_FriendApplyOperation)]
	public partial class C2G_FriendApplyOperation : IRequest {}

	[Message(HotfixOpcode.G2C_FriendApplyOperation)]
	public partial class G2C_FriendApplyOperation : IResponse {}

	[Message(HotfixOpcode.C2G_FriendApplyCount)]
	public partial class C2G_FriendApplyCount : IRequest {}

	[Message(HotfixOpcode.G2C_FriendApplyCount)]
	public partial class G2C_FriendApplyCount : IResponse {}

	[Message(HotfixOpcode.C2G_FriendApplyContent)]
	public partial class C2G_FriendApplyContent : IRequest {}

	[Message(HotfixOpcode.G2C_FriendApplyContent)]
	public partial class G2C_FriendApplyContent : IResponse {}

	[Message(HotfixOpcode.C2G_AddUserMessage)]
	public partial class C2G_AddUserMessage : IRequest {}

	[Message(HotfixOpcode.G2C_AddUserMessage)]
	public partial class G2C_AddUserMessage : IResponse {}

	[Message(HotfixOpcode.C2G_QueryUserMessage)]
	public partial class C2G_QueryUserMessage : IRequest {}

	[Message(HotfixOpcode.G2C_QueryUserMessage)]
	public partial class G2C_QueryUserMessage : IResponse {}

	[Message(HotfixOpcode.C2G_AddChatRoomMessage)]
	public partial class C2G_AddChatRoomMessage : IRequest {}

	[Message(HotfixOpcode.G2C_AddChatRoomMessage)]
	public partial class G2C_AddChatRoomMessage : IResponse {}

	[Message(HotfixOpcode.C2G_QueryChatRoomMessage)]
	public partial class C2G_QueryChatRoomMessage : IRequest {}

	[Message(HotfixOpcode.G2C_QueryChatRoomMessage)]
	public partial class G2C_QueryChatRoomMessage : IResponse {}

	[Message(HotfixOpcode.C2G_AddWalletData)]
	public partial class C2G_AddWalletData : IRequest {}

	[Message(HotfixOpcode.G2C_AddWalletData)]
	public partial class G2C_AddWalletData : IResponse {}

	[Message(HotfixOpcode.C2G_QueryWalletData)]
	public partial class C2G_QueryWalletData : IRequest {}

	[Message(HotfixOpcode.G2C_QueryWalletData)]
	public partial class G2C_QueryWalletData : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateWalletData)]
	public partial class C2G_UpdateWalletData : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateWalletData)]
	public partial class G2C_UpdateWalletData : IResponse {}

	[Message(HotfixOpcode.C2G_AddWalletRecord)]
	public partial class C2G_AddWalletRecord : IRequest {}

	[Message(HotfixOpcode.G2C_AddWalletRecord)]
	public partial class G2C_AddWalletRecord : IResponse {}

	[Message(HotfixOpcode.C2G_QueryWalletRecord)]
	public partial class C2G_QueryWalletRecord : IRequest {}

	[Message(HotfixOpcode.G2C_QueryWalletRecord)]
	public partial class G2C_QueryWalletRecord : IResponse {}

	[Message(HotfixOpcode.C2G_PayGoods)]
	public partial class C2G_PayGoods : IRequest {}

	[Message(HotfixOpcode.G2C_PayGoods)]
	public partial class G2C_PayGoods : IResponse {}

	[Message(HotfixOpcode.C2G_PayHttpMessage)]
	public partial class C2G_PayHttpMessage : IRequest {}

	[Message(HotfixOpcode.G2C_PayHttpMessage)]
	public partial class G2C_PayHttpMessage : IResponse {}

	[Message(HotfixOpcode.C2G_QueryProductInfoDataCount)]
	public partial class C2G_QueryProductInfoDataCount : IRequest {}

	[Message(HotfixOpcode.G2C_QueryProductInfoDataCount)]
	public partial class G2C_QueryProductInfoDataCount : IResponse {}

	[Message(HotfixOpcode.C2G_QueryProductInfoData)]
	public partial class C2G_QueryProductInfoData : IRequest {}

	[Message(HotfixOpcode.G2C_QueryProductInfoData)]
	public partial class G2C_QueryProductInfoData : IResponse {}

	[Message(HotfixOpcode.C2G_AddProductInfoData)]
	public partial class C2G_AddProductInfoData : IRequest {}

	[Message(HotfixOpcode.G2C_AddProductInfoData)]
	public partial class G2C_AddProductInfoData : IResponse {}

	[Message(HotfixOpcode.C2G_DelProductInfoData)]
	public partial class C2G_DelProductInfoData : IRequest {}

	[Message(HotfixOpcode.G2C_DelProductInfoData)]
	public partial class G2C_DelProductInfoData : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateProductInfoData)]
	public partial class C2G_UpdateProductInfoData : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateProductInfoData)]
	public partial class G2C_UpdateProductInfoData : IResponse {}

	[Message(HotfixOpcode.C2G_ViewProductInfoData)]
	public partial class C2G_ViewProductInfoData : IRequest {}

	[Message(HotfixOpcode.G2C_ViewProductInfoData)]
	public partial class G2C_ViewProductInfoData : IResponse {}

	[Message(HotfixOpcode.C2G_ProductInfoDataMessage)]
	public partial class C2G_ProductInfoDataMessage : IRequest {}

	[Message(HotfixOpcode.G2C_ProductInfoDataMessage)]
	public partial class G2C_ProductInfoDataMessage : IResponse {}

	[Message(HotfixOpcode.C2G_AuditProductInfoData)]
	public partial class C2G_AuditProductInfoData : IRequest {}

	[Message(HotfixOpcode.G2C_AuditProductInfoData)]
	public partial class G2C_AuditProductInfoData : IResponse {}

	[Message(HotfixOpcode.C2G_QueryProductInfoOrderCount)]
	public partial class C2G_QueryProductInfoOrderCount : IRequest {}

	[Message(HotfixOpcode.G2C_QueryProductInfoOrderCount)]
	public partial class G2C_QueryProductInfoOrderCount : IResponse {}

	[Message(HotfixOpcode.C2G_QueryProductInfoOrder)]
	public partial class C2G_QueryProductInfoOrder : IRequest {}

	[Message(HotfixOpcode.G2C_QueryProductInfoOrder)]
	public partial class G2C_QueryProductInfoOrder : IResponse {}

	[Message(HotfixOpcode.C2G_AddProductOrderData)]
	public partial class C2G_AddProductOrderData : IRequest {}

	[Message(HotfixOpcode.G2C_AddProductOrderData)]
	public partial class G2C_AddProductOrderData : IResponse {}

	[Message(HotfixOpcode.C2G_AuditProductOrderMessage)]
	public partial class C2G_AuditProductOrderMessage : IRequest {}

	[Message(HotfixOpcode.G2C_AuditProductOrderMessage)]
	public partial class G2C_AuditProductOrderMessage : IResponse {}

	[Message(HotfixOpcode.C2G_DelProductInfoOrder)]
	public partial class C2G_DelProductInfoOrder : IRequest {}

	[Message(HotfixOpcode.G2C_DelProductInfoOrder)]
	public partial class G2C_DelProductInfoOrder : IResponse {}

	[Message(HotfixOpcode.C2G_QuerySimpleOrderCount)]
	public partial class C2G_QuerySimpleOrderCount : IRequest {}

	[Message(HotfixOpcode.G2C_QuerySimpleOrderCount)]
	public partial class G2C_QuerySimpleOrderCount : IResponse {}

	[Message(HotfixOpcode.C2G_QuerySimpleOrder)]
	public partial class C2G_QuerySimpleOrder : IRequest {}

	[Message(HotfixOpcode.G2C_QuerySimpleOrder)]
	public partial class G2C_QuerySimpleOrder : IResponse {}

	[Message(HotfixOpcode.C2G_AddSimpleOrder)]
	public partial class C2G_AddSimpleOrder : IRequest {}

	[Message(HotfixOpcode.G2C_AddSimpleOrder)]
	public partial class G2C_AddSimpleOrder : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateSimpleOrder)]
	public partial class C2G_UpdateSimpleOrder : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateSimpleOrder)]
	public partial class G2C_UpdateSimpleOrder : IResponse {}

	[Message(HotfixOpcode.C2G_DelSimpleOrder)]
	public partial class C2G_DelSimpleOrder : IRequest {}

	[Message(HotfixOpcode.G2C_DelSimpleOrder)]
	public partial class G2C_DelSimpleOrder : IResponse {}

	[Message(HotfixOpcode.C2G_QueryServiceInfoCount)]
	public partial class C2G_QueryServiceInfoCount : IRequest {}

	[Message(HotfixOpcode.G2C_QueryServiceInfoCount)]
	public partial class G2C_QueryServiceInfoCount : IResponse {}

	[Message(HotfixOpcode.C2G_QueryServiceInfo)]
	public partial class C2G_QueryServiceInfo : IRequest {}

	[Message(HotfixOpcode.G2C_QueryServiceInfo)]
	public partial class G2C_QueryServiceInfo : IResponse {}

	[Message(HotfixOpcode.C2G_AddServiceInfo)]
	public partial class C2G_AddServiceInfo : IRequest {}

	[Message(HotfixOpcode.G2C_AddServiceInfo)]
	public partial class G2C_AddServiceInfo : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateServiceInfo)]
	public partial class C2G_UpdateServiceInfo : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateServiceInfo)]
	public partial class G2C_UpdateServiceInfo : IResponse {}

	[Message(HotfixOpcode.C2G_DelServiceInfo)]
	public partial class C2G_DelServiceInfo : IRequest {}

	[Message(HotfixOpcode.G2C_DelServiceInfo)]
	public partial class G2C_DelServiceInfo : IResponse {}

	[Message(HotfixOpcode.C2G_QueryShopInfoDataCount)]
	public partial class C2G_QueryShopInfoDataCount : IRequest {}

	[Message(HotfixOpcode.G2C_QueryShopInfoDataCount)]
	public partial class G2C_QueryShopInfoDataCount : IResponse {}

	[Message(HotfixOpcode.C2G_QueryShopInfoData)]
	public partial class C2G_QueryShopInfoData : IRequest {}

	[Message(HotfixOpcode.G2C_QueryShopInfoData)]
	public partial class G2C_QueryShopInfoData : IResponse {}

	[Message(HotfixOpcode.C2G_AddShopInfoData)]
	public partial class C2G_AddShopInfoData : IRequest {}

	[Message(HotfixOpcode.G2C_AddShopInfoData)]
	public partial class G2C_AddShopInfoData : IResponse {}

	[Message(HotfixOpcode.C2G_DelShopInfoData)]
	public partial class C2G_DelShopInfoData : IRequest {}

	[Message(HotfixOpcode.G2C_DelShopInfoData)]
	public partial class G2C_DelShopInfoData : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateShopInfoData)]
	public partial class C2G_UpdateShopInfoData : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateShopInfoData)]
	public partial class G2C_UpdateShopInfoData : IResponse {}

	[Message(HotfixOpcode.C2G_SUpdateShopInfoData)]
	public partial class C2G_SUpdateShopInfoData : IRequest {}

	[Message(HotfixOpcode.G2C_SUpdateShopInfoData)]
	public partial class G2C_SUpdateShopInfoData : IResponse {}

	[Message(HotfixOpcode.C2G_AuditShopInfoData)]
	public partial class C2G_AuditShopInfoData : IRequest {}

	[Message(HotfixOpcode.C2G_ViewShopInfoData)]
	public partial class C2G_ViewShopInfoData : IRequest {}

	[Message(HotfixOpcode.G2C_ViewShopInfoData)]
	public partial class G2C_ViewShopInfoData : IResponse {}

	[Message(HotfixOpcode.G2C_AuditShopInfoData)]
	public partial class G2C_AuditShopInfoData : IResponse {}

	[Message(HotfixOpcode.C2G_AddShopUserVIP)]
	public partial class C2G_AddShopUserVIP : IRequest {}

	[Message(HotfixOpcode.G2C_AddShopUserVIP)]
	public partial class G2C_AddShopUserVIP : IResponse {}

	[Message(HotfixOpcode.C2G_QueryShopActivityInfoCount)]
	public partial class C2G_QueryShopActivityInfoCount : IRequest {}

	[Message(HotfixOpcode.G2C_QueryShopActivityInfoCount)]
	public partial class G2C_QueryShopActivityInfoCount : IResponse {}

	[Message(HotfixOpcode.C2G_QueryShopActivityInfo)]
	public partial class C2G_QueryShopActivityInfo : IRequest {}

	[Message(HotfixOpcode.G2C_QueryShopActivityInfo)]
	public partial class G2C_QueryShopActivityInfo : IResponse {}

	[Message(HotfixOpcode.C2G_AddShopActivityInfo)]
	public partial class C2G_AddShopActivityInfo : IRequest {}

	[Message(HotfixOpcode.G2C_AddShopActivityInfo)]
	public partial class G2C_AddShopActivityInfo : IResponse {}

	[Message(HotfixOpcode.C2G_UpdateShopActivityInfo)]
	public partial class C2G_UpdateShopActivityInfo : IRequest {}

	[Message(HotfixOpcode.G2C_UpdateShopActivityInfo)]
	public partial class G2C_UpdateShopActivityInfo : IResponse {}

	[Message(HotfixOpcode.C2G_DelShopActivityInfo)]
	public partial class C2G_DelShopActivityInfo : IRequest {}

	[Message(HotfixOpcode.G2C_DelShopActivityInfo)]
	public partial class G2C_DelShopActivityInfo : IResponse {}

	[Message(HotfixOpcode.C2G_AuditShopActivityInfo)]
	public partial class C2G_AuditShopActivityInfo : IRequest {}

	[Message(HotfixOpcode.G2C_AuditShopActivityInfo)]
	public partial class G2C_AuditShopActivityInfo : IResponse {}

	[Message(HotfixOpcode.C2G_DelUserShoppingData)]
	public partial class C2G_DelUserShoppingData : IRequest {}

	[Message(HotfixOpcode.G2C_DelUserShoppingData)]
	public partial class G2C_DelUserShoppingData : IResponse {}

	[Message(HotfixOpcode.C2G_QueryMyProductInfoDataCount)]
	public partial class C2G_QueryMyProductInfoDataCount : IRequest {}

	[Message(HotfixOpcode.G2C_QueryMyProductInfoDataCount)]
	public partial class G2C_QueryMyProductInfoDataCount : IResponse {}

	[Message(HotfixOpcode.C2G_QueryMyShopInfoDataCount)]
	public partial class C2G_QueryMyShopInfoDataCount : IRequest {}

	[Message(HotfixOpcode.G2C_QueryMyShopInfoDataCount)]
	public partial class G2C_QueryMyShopInfoDataCount : IResponse {}

}
namespace ETHotfix
{
	public static partial class HotfixOpcode
	{
		 public const ushort C2R_Login = 10001;
		 public const ushort R2C_Login = 10002;
		 public const ushort C2G_LoginGate = 10003;
		 public const ushort G2C_LoginGate = 10004;
		 public const ushort G2C_TestHotfixMessage = 10005;
		 public const ushort C2M_TestActorRequest = 10006;
		 public const ushort M2C_TestActorResponse = 10007;
		 public const ushort PlayerInfo = 10008;
		 public const ushort C2G_PlayerInfo = 10009;
		 public const ushort G2C_PlayerInfo = 10010;
		 public const ushort C2G_AddAccountInfo = 10011;
		 public const ushort G2C_AddAccountInfo = 10012;
		 public const ushort C2G_UpdateAccountInfo = 10013;
		 public const ushort G2C_UpdateAccountInfo = 10014;
		 public const ushort C2G_QueryAccountInfo = 10015;
		 public const ushort G2C_QueryAccountInfo = 10016;
		 public const ushort C2G_AddGuestAccount = 10017;
		 public const ushort G2C_AddGuestAccount = 10018;
		 public const ushort C2G_UpdateGuestAccount = 10019;
		 public const ushort G2C_UpdateGuestAccount = 10020;
		 public const ushort C2G_QueryGuestAccount = 10021;
		 public const ushort G2C_QueryGuestAccount = 10022;
		 public const ushort C2G_GuestToMainAccount = 10023;
		 public const ushort G2C_GuestToMainAccount = 10024;
		 public const ushort C2G_AddMainAccount = 10025;
		 public const ushort G2C_AddMainAccount = 10026;
		 public const ushort C2G_UpdateMainAccount = 10027;
		 public const ushort G2C_UpdateMainAccount = 10028;
		 public const ushort C2G_QueryMainAccount = 10029;
		 public const ushort G2C_QueryMainAccount = 10030;
		 public const ushort C2G_UserAdress = 10031;
		 public const ushort G2C_UserAdress = 10032;
		 public const ushort C2G_QueryUserAdress = 10033;
		 public const ushort G2C_QueryUserAdress = 10034;
		 public const ushort C2G_AddUserLoginRecord = 10035;
		 public const ushort G2C_AddUserLoginRecord = 10036;
		 public const ushort C2G_QueryUserLoginRecord = 10037;
		 public const ushort G2C_QueryUserLoginRecord = 10038;
		 public const ushort LoginRecord = 10039;
		 public const ushort C2G_AddUserPortrait = 10040;
		 public const ushort G2C_AddUserPortrait = 10041;
		 public const ushort C2G_QueryUserPortrait = 10042;
		 public const ushort G2C_QueryUserPortrait = 10043;
		 public const ushort C2G_UserProductInfo = 10044;
		 public const ushort G2C_UserProductInfo = 10045;
		 public const ushort C2G_QueryUserProductInfo = 10046;
		 public const ushort G2C_QueryUserProductInfo = 10047;
		 public const ushort C2G_AddUserProductRecord = 10048;
		 public const ushort G2C_AddUserProductRecord = 10049;
		 public const ushort C2G_QueryUserProductRecord = 10050;
		 public const ushort G2C_QueryUserProductRecord = 10051;
		 public const ushort ProductRecord = 10052;
		 public const ushort C2G_AddUserRequestRecord = 10053;
		 public const ushort G2C_AddUserRequestRecord = 10054;
		 public const ushort C2G_QueryUserRequestRecord = 10055;
		 public const ushort G2C_QueryUserRequestRecord = 10056;
		 public const ushort RequestRecord = 10057;
		 public const ushort C2G_AddDealOrder = 10058;
		 public const ushort G2C_AddDealOrder = 10059;
		 public const ushort C2G_UpdateDealOrder = 10060;
		 public const ushort G2C_UpdateDealOrder = 10061;
		 public const ushort C2G_QueryDealOrder = 10062;
		 public const ushort G2C_QueryDealOrder = 10063;
		 public const ushort Dealorder = 10064;
		 public const ushort C2G_AddGoodsOrder = 10065;
		 public const ushort G2C_AddGoodsOrder = 10066;
		 public const ushort C2G_UpdateGoodsOrder = 10067;
		 public const ushort G2C_UpdateGoodsOrder = 10068;
		 public const ushort C2G_QueryGoodsOrder = 10069;
		 public const ushort G2C_QueryGoodsOrder = 10070;
		 public const ushort C2G_UpdateChatRoomMessage = 10071;
		 public const ushort G2C_UpdateChatRoomMessage = 10072;
		 public const ushort C2G_AddFriendInfo = 10073;
		 public const ushort G2C_AddFriendInfo = 10074;
		 public const ushort C2G_UpdateFriendInfo = 10075;
		 public const ushort G2C_UpdateFriendInfo = 10076;
		 public const ushort C2G_QueryFriendInfo = 10077;
		 public const ushort G2C_QueryFriendInfo = 10078;
		 public const ushort C2G_AddGroupInfo = 10079;
		 public const ushort G2C_AddGroupInfo = 10080;
		 public const ushort C2G_UpdateGroupInfo = 10081;
		 public const ushort G2C_UpdateGroupInfo = 10082;
		 public const ushort C2G_QueryGroupInfo = 10083;
		 public const ushort G2C_QueryGroupInfo = 10084;
		 public const ushort C2G_AddRelationInfo = 10085;
		 public const ushort G2C_AddRelationInfo = 10086;
		 public const ushort C2G_UpdateRelationInfo = 10087;
		 public const ushort G2C_UpdateRelationInfo = 10088;
		 public const ushort C2G_QueryRelationInfo = 10089;
		 public const ushort G2C_QueryRelationInfo = 10090;
		 public const ushort C2G_AddRequestInfo = 10091;
		 public const ushort G2C_AddRequestInfo = 10092;
		 public const ushort C2G_UpdateRequestInfo = 10093;
		 public const ushort G2C_UpdateRequestInfo = 10094;
		 public const ushort C2G_QueryRequestInfo = 10095;
		 public const ushort G2C_QueryRequestInfo = 10096;
		 public const ushort C2G_UpdateUserMessage = 10097;
		 public const ushort G2C_UpdateUserMessage = 10098;
		 public const ushort C2G_UpdateWalletRecord = 10099;
		 public const ushort G2C_UpdateWalletRecord = 10100;
		 public const ushort C2R_RegistForSaveAccount = 10101;
		 public const ushort R2C_RegistForSaveAccount = 10102;
		 public const ushort C2R_FindPassword = 10103;
		 public const ushort R2C_FindPassword = 10104;
		 public const ushort C2R_CompareAccount = 10105;
		 public const ushort R2C_CompareAccount = 10106;
		 public const ushort C2R_ForSaveInformation = 10107;
		 public const ushort R2C_ForSaveInformation = 10108;
		 public const ushort C2G_ChangePassword = 10109;
		 public const ushort G2C_ChangePassword = 10110;
		 public const ushort C2G_GetPersonalInformation = 10111;
		 public const ushort G2C_GetPersonalInformation = 10112;
		 public const ushort C2G_AddressManager = 10113;
		 public const ushort G2C_AddressManager = 10114;
		 public const ushort C2G_LoginRecord = 10115;
		 public const ushort G2C_LoginRecord = 10116;
		 public const ushort C2G_FriendList = 10117;
		 public const ushort G2C_FriendList = 10118;
		 public const ushort C2G_AddFriendID = 10119;
		 public const ushort G2C_AddFriendID = 10120;
		 public const ushort C2G_UpdateFriendID = 10121;
		 public const ushort G2C_UpdateFriendID = 10122;
		 public const ushort C2G_FriendInfoDateMessage = 10123;
		 public const ushort G2C_FriendInfoDateMessage = 10124;
		 public const ushort C2G_AddBlackID = 10125;
		 public const ushort G2C_AddBlackID = 10126;
		 public const ushort C2G_AddGroup = 10127;
		 public const ushort G2C_AddGroup = 10128;
		 public const ushort C2G_UpdateGroup = 10129;
		 public const ushort G2C_UpdateGroup = 10130;
		 public const ushort C2G_AddChatRoom = 10131;
		 public const ushort G2C_AddChatRoom = 10132;
		 public const ushort C2G_DeleteChatRoom = 10133;
		 public const ushort G2C_DeleteChatRoom = 10134;
		 public const ushort C2G_JoinChatRoom = 10135;
		 public const ushort G2C_JoinChatRoom = 10136;
		 public const ushort C2G_UpdateChatRoom = 10137;
		 public const ushort G2C_UpdateChatRoom = 10138;
		 public const ushort C2G_QueryChatRoom = 10139;
		 public const ushort G2C_QueryChatRoom = 10140;
		 public const ushort C2G_FriendApply = 10141;
		 public const ushort G2C_FriendApply = 10142;
		 public const ushort C2G_FriendApplyOperation = 10143;
		 public const ushort G2C_FriendApplyOperation = 10144;
		 public const ushort C2G_FriendApplyCount = 10145;
		 public const ushort G2C_FriendApplyCount = 10146;
		 public const ushort C2G_FriendApplyContent = 10147;
		 public const ushort G2C_FriendApplyContent = 10148;
		 public const ushort C2G_AddUserMessage = 10149;
		 public const ushort G2C_AddUserMessage = 10150;
		 public const ushort C2G_QueryUserMessage = 10151;
		 public const ushort G2C_QueryUserMessage = 10152;
		 public const ushort C2G_AddChatRoomMessage = 10153;
		 public const ushort G2C_AddChatRoomMessage = 10154;
		 public const ushort C2G_QueryChatRoomMessage = 10155;
		 public const ushort G2C_QueryChatRoomMessage = 10156;
		 public const ushort C2G_AddWalletData = 10157;
		 public const ushort G2C_AddWalletData = 10158;
		 public const ushort C2G_QueryWalletData = 10159;
		 public const ushort G2C_QueryWalletData = 10160;
		 public const ushort C2G_UpdateWalletData = 10161;
		 public const ushort G2C_UpdateWalletData = 10162;
		 public const ushort C2G_AddWalletRecord = 10163;
		 public const ushort G2C_AddWalletRecord = 10164;
		 public const ushort C2G_QueryWalletRecord = 10165;
		 public const ushort G2C_QueryWalletRecord = 10166;
		 public const ushort C2G_PayGoods = 10167;
		 public const ushort G2C_PayGoods = 10168;
		 public const ushort C2G_PayHttpMessage = 10169;
		 public const ushort G2C_PayHttpMessage = 10170;
		 public const ushort C2G_QueryProductInfoDataCount = 10171;
		 public const ushort G2C_QueryProductInfoDataCount = 10172;
		 public const ushort C2G_QueryProductInfoData = 10173;
		 public const ushort G2C_QueryProductInfoData = 10174;
		 public const ushort C2G_AddProductInfoData = 10175;
		 public const ushort G2C_AddProductInfoData = 10176;
		 public const ushort C2G_DelProductInfoData = 10177;
		 public const ushort G2C_DelProductInfoData = 10178;
		 public const ushort C2G_UpdateProductInfoData = 10179;
		 public const ushort G2C_UpdateProductInfoData = 10180;
		 public const ushort C2G_ViewProductInfoData = 10181;
		 public const ushort G2C_ViewProductInfoData = 10182;
		 public const ushort C2G_ProductInfoDataMessage = 10183;
		 public const ushort G2C_ProductInfoDataMessage = 10184;
		 public const ushort C2G_AuditProductInfoData = 10185;
		 public const ushort G2C_AuditProductInfoData = 10186;
		 public const ushort C2G_QueryProductInfoOrderCount = 10187;
		 public const ushort G2C_QueryProductInfoOrderCount = 10188;
		 public const ushort C2G_QueryProductInfoOrder = 10189;
		 public const ushort G2C_QueryProductInfoOrder = 10190;
		 public const ushort C2G_AddProductOrderData = 10191;
		 public const ushort G2C_AddProductOrderData = 10192;
		 public const ushort C2G_AuditProductOrderMessage = 10193;
		 public const ushort G2C_AuditProductOrderMessage = 10194;
		 public const ushort C2G_DelProductInfoOrder = 10195;
		 public const ushort G2C_DelProductInfoOrder = 10196;
		 public const ushort C2G_QuerySimpleOrderCount = 10197;
		 public const ushort G2C_QuerySimpleOrderCount = 10198;
		 public const ushort C2G_QuerySimpleOrder = 10199;
		 public const ushort G2C_QuerySimpleOrder = 10200;
		 public const ushort C2G_AddSimpleOrder = 10201;
		 public const ushort G2C_AddSimpleOrder = 10202;
		 public const ushort C2G_UpdateSimpleOrder = 10203;
		 public const ushort G2C_UpdateSimpleOrder = 10204;
		 public const ushort C2G_DelSimpleOrder = 10205;
		 public const ushort G2C_DelSimpleOrder = 10206;
		 public const ushort C2G_QueryServiceInfoCount = 10207;
		 public const ushort G2C_QueryServiceInfoCount = 10208;
		 public const ushort C2G_QueryServiceInfo = 10209;
		 public const ushort G2C_QueryServiceInfo = 10210;
		 public const ushort C2G_AddServiceInfo = 10211;
		 public const ushort G2C_AddServiceInfo = 10212;
		 public const ushort C2G_UpdateServiceInfo = 10213;
		 public const ushort G2C_UpdateServiceInfo = 10214;
		 public const ushort C2G_DelServiceInfo = 10215;
		 public const ushort G2C_DelServiceInfo = 10216;
		 public const ushort C2G_QueryShopInfoDataCount = 10217;
		 public const ushort G2C_QueryShopInfoDataCount = 10218;
		 public const ushort C2G_QueryShopInfoData = 10219;
		 public const ushort G2C_QueryShopInfoData = 10220;
		 public const ushort C2G_AddShopInfoData = 10221;
		 public const ushort G2C_AddShopInfoData = 10222;
		 public const ushort C2G_DelShopInfoData = 10223;
		 public const ushort G2C_DelShopInfoData = 10224;
		 public const ushort C2G_UpdateShopInfoData = 10225;
		 public const ushort G2C_UpdateShopInfoData = 10226;
		 public const ushort C2G_SUpdateShopInfoData = 10227;
		 public const ushort G2C_SUpdateShopInfoData = 10228;
		 public const ushort C2G_AuditShopInfoData = 10229;
		 public const ushort C2G_ViewShopInfoData = 10230;
		 public const ushort G2C_ViewShopInfoData = 10231;
		 public const ushort G2C_AuditShopInfoData = 10232;
		 public const ushort C2G_AddShopUserVIP = 10233;
		 public const ushort G2C_AddShopUserVIP = 10234;
		 public const ushort C2G_QueryShopActivityInfoCount = 10235;
		 public const ushort G2C_QueryShopActivityInfoCount = 10236;
		 public const ushort C2G_QueryShopActivityInfo = 10237;
		 public const ushort G2C_QueryShopActivityInfo = 10238;
		 public const ushort C2G_AddShopActivityInfo = 10239;
		 public const ushort G2C_AddShopActivityInfo = 10240;
		 public const ushort C2G_UpdateShopActivityInfo = 10241;
		 public const ushort G2C_UpdateShopActivityInfo = 10242;
		 public const ushort C2G_DelShopActivityInfo = 10243;
		 public const ushort G2C_DelShopActivityInfo = 10244;
		 public const ushort C2G_AuditShopActivityInfo = 10245;
		 public const ushort G2C_AuditShopActivityInfo = 10246;
		 public const ushort C2G_DelUserShoppingData = 10247;
		 public const ushort G2C_DelUserShoppingData = 10248;
		 public const ushort C2G_QueryMyProductInfoDataCount = 10249;
		 public const ushort G2C_QueryMyProductInfoDataCount = 10250;
		 public const ushort C2G_QueryMyShopInfoDataCount = 10251;
		 public const ushort G2C_QueryMyShopInfoDataCount = 10252;
	}
}
