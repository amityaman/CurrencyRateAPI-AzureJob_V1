USE [CurrencyDB]
GO
/****** Object:  Table [dbo].[CurrencyRate]    Script Date: 10/15/2017 2:08:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrencyRate](
	[CurrencyRateID] [int] IDENTITY(1,1) NOT NULL,
	[FromCurrencyCode] [Varchar](28) NOT NULL,
	[ExchangeRate] [decimal](20, 4) NULL,
	[CurrencyRateDate] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[procExchRateCreate]    Script Date: 10/15/2017 2:08:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[procExchRateCreate]
( 
	@pstrCurrCode VARCHAR(28), 
	@pdecExchRate Decimal(20,4)
)
/*
Procedure to Insert the CurrencyRate
Created By : Amit Jain
Created On : 
*/
AS

	Declare @dteServerDate DateTime	
	

	Select @dteServerDate = GetDate()

	
			INSERT INTO CurrencyRate
			(
				FromCurrencyCode,ExchangeRate,CurrencyRateDate
			)
			VALUES
			(
				 @pstrCurrCode, @pdecExchRate, @dteServerDate
			)


	






GO
/****** Object:  StoredProcedure [dbo].[procExchRateSelect]    Script Date: 10/15/2017 2:08:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procExchRateSelect]
( 
	
	@pstrCurrCode varchar(28)
	
	
)
/*
Procedure to Select the [CurrencyRate] by Currency code
Created By : Amit Jain
Created On : 
*/
AS
	Select a.FromCurrencyCode,a.ExchangeRate,a.CurrencyRateDate from CurrencyRate a 
	 where a.FromCurrencyCode = @pstrCurrCode
	 and a.CurrencyRateDate = 
	 (select Max(CurrencyRateDate) from CurrencyRate where FromCurrencyCode = a.FromCurrencyCode)


GO
