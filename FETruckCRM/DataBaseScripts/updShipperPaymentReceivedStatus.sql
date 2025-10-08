ALTER proc [dbo].[updShipperPaymentReceivedStatus]                
(                
@LoadID bigint,                
@IsShipperPaymentReceived bit  ,              
@ShipperPaymentReceivedDate varchar(100),      
@LoggedInUserId varchar(5),  
@ShipperReferenceNo varchar(50),  
@ShipperPaymentUrl varchar(500)  
)                
AS                
BEGIN             
 BEGIN TRY           
-- Check load shipper status            
declare @ApprovalStatus int            
declare @CustomerID bigint            
declare @LoadRatePercent numeric(18,2),@Rate numeric(18,2)         
select @Rate=ISNULL(RatePercent,0) from tbLoad where LoadID=@LoadID            
select @CustomerID=CustomerID, @ApprovalStatus= ApprovalStatus from tbCustomer where CustomerID=(select top 1 billto from tbLoad where LoadID=@LoadID)               
update tbLoad set IsShipperPaymentReceived=@IsShipperPaymentReceived , ShipperPaymentReceiveddate=@ShipperPaymentReceivedDate,      
ShipperPaymentUpdateBy = @LoggedInUserId,ShipperReferenceNo= @ShipperReferenceNo,ShipperPaymentUrl = @ShipperPaymentUrl     
where LoadID=@LoadID        
if(@ApprovalStatus=4 and @IsShipperPaymentReceived=1)            
BEGIN            
update tbCustomer set CreditLimit=(ISNULL(CreditLimit,0)-@Rate) where CustomerID=@CustomerID            
END 
declare @ShipperInvoivceSentDate datetime;
select @ShipperInvoivceSentDate=ShipperInvoiceSentDate from tbLoad where LoadId=@LoadID
if(DATEDIFF(day,@ShipperInvoivceSentDate,@ShipperPaymentReceivedDate)<=60)
BEGIN
update tbCustomer set IsOnHold=0 where CustomerID=@CustomerID
END
 


            
select @LoadID            
END TRY          
BEGIN CATCH          
select -1          
END CATCH          
          
END 

