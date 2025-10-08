--[GetAllLoadsByPaging] '' ,0,100000,6,'Loadno',' desc'                        
ALTER PROCEDURE [dbo].[GetAllLoadsByPaging]                          
(      
    
 @Search NVARCHAR(500) = '',                          
 @DisplayStart INT = 0,                          
 @DisplayLength INT = 10000000,                          
 @UserID bigint =1,                        
 @SortCol varchar(100)='Loadno',                        
 @Sortdir varchar(100)='desc'                        
)                          
AS BEGIN                          
    SET NOCOUNT ON;                          
    SET @Search = LTRIM(RTRIM(@Search))                          
 DECLARE @Role varchar(100)                                                        
                                                    
 SELECT top 1   @Role= TR.Role                                                     
 FROM   tbusers AS U INNER JOIN                                                          
  tbUserRole AS R ON U.UserID = R.UserID                                                     
 inner join tbRole TR on TR.RoleID=R.RoleID                                                    
 WHERE        (U.Isdeleted = 0) AND (U.UserID=@UserID)                                                    
                                                    
 DECLARE @RoleID int                                                          
                                                        
 SELECT @RoleID=ISNULL(RoleID,0) FROM tbUserRole WHERE UserID=@USerID                    
                                                         
 if(@RoleID=0 or @RoleId=1 or @RoleId=2)                                                        
 BEGIN                                                        
 SET @UserID=0                                                        
 END                              
                        
;WITH CTE_Results AS                           
(                          
                        
SELECT                          
 case                        
when @SortCol='LoadID'  and @Sortdir='asc' then  Row_number() over(order by  ISNULL(L.LoadID,0)  asc)                          
when @SortCol='LoadID'  and @Sortdir='desc' then  Row_number() over(order by  ISNULL(L.LoadID,0)  desc)                 
when @SortCol='LoadNo'  and @Sortdir='asc' then  Row_number() over(order by  ISNULL(L.LoadNo,0)  asc)                          
when @SortCol='LoadNo'  and @Sortdir='desc' then  Row_number() over(order by  ISNULL(L.LoadNo,0)  desc)               
when @SortCol='InvoiceNo' and @Sortdir='asc' then Row_number() over(order by  L.InvoiceNo asc)                        
when @SortCol='InvoiceNo' and @Sortdir='desc' then Row_number() over(order by  L.InvoiceNo desc)                        
when @SortCol='WO' and @Sortdir='asc' then Row_number() over(order by  L.WO asc)                        
when @SortCol='WO' and @Sortdir='desc' then Row_number() over(order by  L.WO desc)                        
when @SortCol='CareerName' and @Sortdir='asc' then Row_number() over(order by  CR.CareerName asc)                        
when @SortCol='CareerName' and @Sortdir='desc' then Row_number() over(order by  CR.CareerName desc)                        
when @SortCol='strShipperDate' and @Sortdir='asc' then Row_number() over(order by  LS.ShipperDate asc)                        
when @SortCol='strShipperDate' and @Sortdir='desc' then Row_number() over(order by  LS.ShipperDate desc)                        
when @SortCol='CreatedDate' and @Sortdir='asc' then Row_number() over(order by  L.CreatedDate asc)                        
when @SortCol='CreatedDate' and @Sortdir='desc' then Row_number() over(order by  L.CreatedDate desc)                        
when @SortCol='strDeliveredDate' and @Sortdir='asc' then Row_number() over(order by   LC.ConsigneeDate asc)                        
when @SortCol='strDeliveredDate' and @Sortdir='desc' then Row_number() over(order by   LC.ConsigneeDate desc)                        
when @SortCol='CustomerName' and @Sortdir='asc' then Row_number() over(order by  CR.CareerName asc)                        
when @SortCol='CustomerName' and @Sortdir='desc' then Row_number() over(order by  CR.CareerName desc)                        
when @SortCol='Location' and @Sortdir='asc'  then Row_number() over(order by  LS.Location asc)                        
when @SortCol='Location' and @Sortdir='desc'  then Row_number() over(order by  LS.Location desc)                        
when @SortCol='ConsigneeLocation'and @Sortdir='asc' then Row_number() over(order by  LC.ConsigneeLocation asc)                        
when @SortCol='ConsigneeLocation'and @Sortdir='desc' then Row_number() over(order by  LC.ConsigneeLocation desc)                        
when @SortCol='strRate' and @Sortdir='asc' then Row_number() over(order by  L.RatePercent asc)                        
when @SortCol='strRate' and @Sortdir='desc'  then Row_number() over(order by  L.RatePercent desc)                        
when @SortCol='strCareerFee' and @Sortdir='asc' then Row_number() over(order by  L.FinalCarrierFee asc)                        
when @SortCol='strCareerFee' and @Sortdir='desc' then Row_number() over(order by  L.FinalCarrierFee desc)                        
when @SortCol='Status' and @Sortdir='asc' then Row_number() over(order by  L.Status asc)                        
when @SortCol='Status' and @Sortdir='desc' then Row_number() over(order by  L.Status desc)                        
else Row_number() over(order by  L.LoadID  desc)                        
end  as RowNo,                           
L.LoadID,                        
ISNULL(L.LoadNo,0) as LoadNo,                        
L.InvoiceNo,                        
ISNULL(L.LoadType,1) as LoadType,                        
@Role Role ,                                                  
L.BillTo,                        
L.Status,                        
L.WO,                        
ISNULL(L.RatePercent,0) as Rate,                        
L.FinalCarrierFee as CareerFee,                        
Convert(varchar(10), L.CreatedDate,101) CreatedDate,                        
L.IsDeletedInd,                        
CR.CareerName,                        
Convert(varchar(10),LS.ShipperDate,101) ShipperDate ,                                                       
Convert(varchar(10),LC.ConsigneeDate,101) ConsigneeDate,                        
C.CustomerName AS CustomerName,                        
LS.Location,                        
LC.ConsigneeLocation,                        
ISNULL(IsShipperPaymentReceived,0) IsShipperPaymentReceived,                                              
ISNULL(IsShipperInvoiceSent,0) IsShipperInvoiceSent,                                              
ISNULL(IsCarrierInvoiceReceived,0) IsCarrierInvoiceReceived,                                              
ISNULL(IsCarrierPaymentMade,0) IsCarrierPaymentMade,                                      
ISNULL(Convert(varchar(10),InvoiceDate,101),'') InvoiceDate  ,                                            
ISNULL(Convert(varchar(10),ShipperInvoiceSentDate,101),'') ShipperInvoiceSentDate  ,                                            
ISNULL(Convert(varchar(10),ShipperPaymentReceivedDate,101),'') ShipperPaymentReceivedDate ,                                             
ISNULL(Convert(varchar(10),CarrierInvoiceReceivedDate,101),'') CarrierInvoiceReceivedDate  ,                                            
ISNULL(Convert(varchar(10),CarrierPaymentMadeDate,101),'') CarrierPaymentMadeDate,          
ISNULL(PaymentType,0) PaymentType,
ISNULL(Convert(varchar(10),inGateEntryDate,101),'')inGateEntryDate,                                             
ISNULL (L.ReceipetUrl, '')  AS ReceipetUrl ,                        
ISNULL (L.ShipperPaymentUrl, '')  AS ShipperPaymentUrl ,                                
ISNULL (L.CarrierInvoiceUrl, '')  AS CarrierInvoiceUrl,          
--ISNULL (L.CarrierPaymentUrl, '')  AS CarrierPaymentUrl,          
L.MCRefNo  ,   
CarrierReferenceNo,        
ShipperReferenceNo   
                                                     
FROM  tbLoad AS L     
INNER JOIN tbCustomer AS C ON L.BillTo = C.CustomerID    
LEFT OUTER JOIN tbCareer AS CR ON CR.CareerID = L.CareerID    
LEFT OUTER JOIN                                                          
    (SELECT  LoadID, Min(LoadShipperID) as LoadShipperID--, IsDeletedInd ,Location,ShipperDate      
 FROM tbLoadShipper     WHERE IsDeletedInd = 0 group by LoadID) as LS1 ON LS1.LoadID = L.LoadID    
 INNER JOIN tbLoadShipper LS On Ls.LoadShipperID=LS1.LoadShipperID   
 LEFT OUTER JOIN      (SELECT LoadID,Max(LoadConSigneeID) as  LoadConSigneeID--, IsDeletedInd ,ConsigneeLocation,ConsigneeDate    
FROM tbLoadConsignee     WHERE IsDeletedInd = 0 group by  LoadID) as LC1 ON LC1.LoadID = L.LoadID    
 INNER JOIN tbLoadConsignee LC on Lc.LoadConsigneeID=LC1.LoadConSigneeID    
    
INNER JOIN  tbusers AS U ON U.UserID = L.CreatedByID                        
INNER JOIN  tbLoadStatus AS LStatus ON LStatus.LoadStatusID = L.Status                        
LEFT OUTER JOIN tbusers AS U1 ON U.TeamLead = U1.UserID                        
LEFT OUTER JOIN  tbusers AS U2 ON U.TeamManager = U2.UserID                                                          
WHERE (L.IsDeletedInd = 0)                       
AND (L.CreatedByID = @UserID       
 OR L.CreatedByID IN (SELECT UserID FROM tbusers  WHERE(TeamLead = @UserID))      
 OR (L.CreatedByID IN   (SELECT UserID FROM  tbusers  WHERE (TeamManager = @UserID))      
 OR (@UserID = 0)       
 OR   L.CreatedByID in  ( select UserID from tbusers where TeamManager in (select USerID from tbusers where TeamManager=@UserID) )      
 OR @Role='Accounting' OR @Role='Compliance Auditor' OR @Role='SuperAdmin' or @Role='Administrator'))                                                  
AND (L.Status <>11)                                              
AND (L.LoadNo like '%'+@Search+'%' OR L.InvoiceNo like '%'+@Search+'%' OR C.CustomerName like '%'+@Search+'%'                        
 OR L.WO like '%'+@Search+'%'      
 OR LS.Location like '%'+@Search+'%'     
 OR LC.ConsigneeLocation like '%'+@Search+'%'                        
 OR @Search=''                        
   )        
AND L.CreatedDate>=  Case When  @Role='SuperAdmin' OR @Role='Administrator' then (select MIN(tbLoad.CreatedDate) from tbLoad)  else DATEADD(dd,-180,getdate()) end      
)                         
                        
 Select  * into #temp from CTE_Results                        
                        
 select * from #temp where RowNo>@DisplayStart and RowNo<=(@DisplayStart+@DisplayLength)                        
 select Count(*) as TotalRows from #temp                        
                        
 drop table #temp                        
                            
END   