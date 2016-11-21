using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Functions
    {
        public enum TransferType
        {
            Replace = 0,
            Union = 1,
            Diff = 2
        }

        public delegate void InsertProgressEventHandler(object sender,
                                                  InsertProgressEventArgs e);

        public event InsertProgressEventHandler InsertProgress;

        protected virtual void OnInsertProgress(InsertProgressEventArgs e)
        {
            if (InsertProgress != null)
                InsertProgress(this, e);
        }

        public SQLiteConnection CreateConnection(string path)
        {
            return new SQLiteConnection(String.Format("Data Source={0};Version=3;", path));
        }

        public List<String> LoadDataBaseTables(String path)
        {
            SQLiteConnection cn;
            SQLiteDataAdapter db;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            List<String> tables = new List<string>();
            
            cn = CreateConnection(path);
            cn.Open();

            string sql = "SELECT name FROM sqlite_master WHERE type='table';";

            SQLiteCommand command = new SQLiteCommand(sql, cn);

            db = new SQLiteDataAdapter(sql, cn);
            ds.Reset();
            db.Fill(ds);
            dt = ds.Tables[0];

            foreach(System.Data.DataRow row in dt.Rows)
            {
                tables.Add(row.ItemArray[0].ToString());
            }

            return tables;
        }
        
        public bool RunCustomScripts (string path)
        {
            List<string> sourceModel = new List<string>()
            {

                "ALTER TABLE CAT_SKU ADD COD_EX_TIPI VARCHAR(3) NULL",
                "ALTER TABLE CAT_SKU_EMBALAGEM ADD PORC_ABSORCAO_DESCONTO NUMERIC(5,2) NULL",
                "ALTER TABLE LJV_ATENDIMENTO_ITEM ADD ID_SKU_SOLICITADO INT NULL",
                "ALTER TABLE LJV_ATENDIMENTO_ACAO_PROMOCAO ADD INDICA_ATENDIMENTO_OFFLINE BIT NULL",
                "ALTER TABLE LJV_ATENDIMENTO_ACAO_PROMOCAO ADD QTDE_PINCODES INT NULL",
                "ALTER TABLE LJV_ATENDIMENTO_DOC_FISCAL ADD LX_TIPO_DOCUMENTO TINYINT NULL",
                "ALTER TABLE LJV_CAIXA_FECHAMENTO ADD JUSTIFICATIVA varchar(200) NULL",
                "ALTER TABLE LJV_TERMINAL ADD ECF_PORTA VARCHAR(6) NULL",
                "ALTER TABLE LJV_TERMINAL ADD ECF_VELOCIDADE INT NULL",
                "ALTER TABLE LJV_TERMINAL ADD TEF_TERMINAL VARCHAR(12) NULL",
                "ALTER TABLE LJV_TERMINAL ADD TEF_PORTA_PINPAD VARCHAR(6) NULL",
                "ALTER TABLE CAT_SKU ADD INATIVO BIT NULL",
                "ALTER TABLE CAT_SKU_PRECO ADD COLUMN LX_HASH VARCHAR(32) NULL",
                "UPDATE CAT_SKU SET INATIVO = 0 WHERE INATIVO IS NULL",
                "CREATE TABLE CAT_SKU_TRIBUTO(ID_CAT_SKU_TRIBUTO int NOT NULL, ID_SKU int NOT NULL, DATA_INICIO_VIGENCIA datetime NULL, TAXA_TRIBUTOS_FEDERAL numeric(10, 2) NOT NULL, TAXA_TRIBUTOS_ESTADUAL numeric(10, 2) NOT NULL, TAXA_TRIBUTOS_MUNICIPAL numeric(10, 2) NOT NULL, CHAVE_IBPT varchar(10) NOT NULL, FONTE varchar(40) NOT NULL, CONSTRAINT XPK_CAT_SKU_TRIBUTO PRIMARY KEY (ID_CAT_SKU_TRIBUTO, ID_SKU), CONSTRAINT XFK_CAT_SKU_TRIBUTO_CAT_SKU FOREIGN KEY (ID_SKU)  REFERENCES CAT_SKU(ID_SKU))",
                "ALTER TABLE STK_ROMANEIO_AJUSTE ADD DATA_ROMANEIO DATETIME NULL",
                "ALTER TABLE LJV_ATENDIMENTO ADD VALOR_DESCONTO_ITENS NUMERIC(14, 2) NULL",
                "CREATE INDEX IDX_CAT_REGRA_FISCAL_SKU ON CAT_REGRA_FISCAL (ID_SKU)",
                "CREATE INDEX IDX_CAT_REGRA_FISCAL_IMPOSTO_ID ON CAT_REGRA_FISCAL_IMPOSTO (ID_REGRA_FISCAL_IMPOSTO)",
                "CREATE INDEX IDX_CAT_SKU_COD ON CAT_SKU (COD_SKU)",
                "CREATE INDEX IDX_CAT_SKU_ID ON CAT_SKU (ID_SKU)",
                "CREATE INDEX IDX_CAT_SKU_CODIGO_BARRA ON CAT_SKU_CODIGO_BARRA (CODIGO_BARRA ASC)",
                "CREATE INDEX IDX_CAT_SKU_PRECO ON CAT_SKU_PRECO (ID_SKU, ID_SKU_PRECO, ID_TAB_PRECO, INATIVO)",
                "CREATE INDEX IDX_CAT_SKU_DESC ON CAT_SKU (DESC_SKU)",

                @"ALTER TABLE CAT_REGRA_FISCAL_IMPOSTO ADD LX_TIPO_REGRA smallint DEFAULT 1 NOT NULL;", // Para a criação correta da tabela CAT_REGRA_FISCAL é necessário a solicitação de um novo banco ao UX. Rafael Fernandes 11/07
                @"ALTER TABLE CAT_REGRA_FISCAL ADD LX_TIPO_REGRA smallint DEFAULT 1 NOT NULL;",

                "DROP VIEW CAT_VW_REGRA_ICMS",
                    @"CREATE VIEW CAT_VW_REGRA_ICMS AS
					       SELECT K.ID_SKU,
							      KK.ALIQUOTA_IMPOSTO
						     FROM CAT_REGRA_FISCAL K
							      JOIN CAT_REGRA_FISCAL_IMPOSTO KK
								    ON K.ID_REGRA_FISCAL_IMPOSTO = KK.ID_REGRA_FISCAL_IMPOSTO 
                                    AND K.LX_TIPO_REGRA = KK.LX_TIPO_REGRA
					       AND
					       KK.ID_IMPOSTO =( 
						       SELECT VALOR_PARAMETRO
							     FROM LJV_PARAMETRO
							    WHERE TITULO_PARAMETRO = 'ID_IMPOSTO_ICMS'
							    LIMIT 1 
					   );",
                "DROP VIEW CAT_VW_SKU",
                @"CREATE VIEW CAT_VW_SKU AS
					   SELECT DISTINCT A.ID_SKU AS ID_SKU,
							  A.ID_ARTIGO AS ID_ARTIGO,
							  B.DESC_ARTIGO AS DESC_ARTIGO,
							  B.COD_ARTIGO AS COD_ARTIGO,
							  A.COD_ITEM_FISCAL AS COD_ITEM_FISCAL,
							  A.COD_SKU AS COD_SKU,
							  A.DESC_SKU AS DESC_SKU,
							  A.DESC_ITEM_FISCAL AS DESC_ITEM_FISCAL,							  
							  A.MULTIPLO_UNIDADE AS MULTIPLO_UNIDADE,
							  A.LX_EMBALAGEM AS LX_EMBALAGEM,
							  A.ORDEM_VISUALIZACAO AS ORDEM_VISUALIZACAO,
							  A.DESMEMBRA_ITEM_VENDA AS DESMEMBRA_ITEM_VENDA,
							  A.DESMEMBRA_ITEM_FISCAL AS DESMEMBRA_ITEM_FISCAL,
							  A.DESMEMBRA_ITEM_ESTOQUE AS DESMEMBRA_ITEM_ESTOQUE,
							  A.INDICA_NUMERO_SERIE AS INDICA_NUMERO_SERIE,
							  A.PESO_BRUTO AS PESO_BRUTO,
							  A.ID_ARTIGO_VARIANTE_VALOR_01 AS ID_ARTIGO_VARIANTE_VALOR_01,
							  C.DESC_ATRIBUTO AS DESC_ARTIGO_VARIANTE_VALOR_01,
							  C.DESC_ATRIBUTO_RESUMIDO AS DESC_VARIANTE_RESUMIDO_01,
							  C.ID_ATRIBUTO AS ID_ATRIBUTO_VARIANTE_01,
							  A.ID_ARTIGO_VARIANTE_VALOR_02 AS ID_ARTIGO_VARIANTE_VALOR_02,
							  D.DESC_ATRIBUTO AS DESC_ARTIGO_VARIANTE_VALOR_02,
							  D.DESC_ATRIBUTO_RESUMIDO AS DESC_VARIANTE_RESUMIDO_02,
							  D.ID_ATRIBUTO AS ID_ATRIBUTO_VARIANTE_02,
							  A.ID_ARTIGO_VARIANTE_VALOR_03 AS ID_ARTIGO_VARIANTE_VALOR_03,
							  E.DESC_ATRIBUTO AS DESC_ARTIGO_VARIANTE_VALOR_03,
							  E.DESC_ATRIBUTO_RESUMIDO AS DESC_VARIANTE_RESUMIDO_03,
							  E.ID_ATRIBUTO AS ID_ATRIBUTO_VARIANTE_03,
							  A.ID_ARTIGO_VARIANTE_VALOR_04 AS ID_ARTIGO_VARIANTE_VALOR_04,
							  F.DESC_ATRIBUTO AS DESC_ARTIGO_VARIANTE_VALOR_04,
							  F.DESC_ATRIBUTO_RESUMIDO AS DESC_VARIANTE_RESUMIDO_04,
							  F.ID_ATRIBUTO AS ID_ATRIBUTO_VARIANTE_04,
							  A.ID_ARTIGO_VARIANTE_VALOR_05 AS ID_ARTIGO_VARIANTE_VALOR_05,
							  G.DESC_ATRIBUTO AS DESC_ARTIGO_VARIANTE_VALOR_05,
							  G.DESC_ATRIBUTO_RESUMIDO AS DESC_VARIANTE_RESUMIDO_05,
							  G.ID_ATRIBUTO AS ID_ATRIBUTO_VARIANTE_05,
							  B.LX_INDICADOR_FISCAL_MERCADORIA AS LX_INDICADOR_FISCAL_MERCADORIA,
							  A.PESO_LIQUIDO AS PESO_LIQUIDO,
							  A.STK_SALDO_QTDE AS STK_SALDO_QTDE,
							  I.URL AS URL,
							  A.COD_CLASSIF_FISCAL,
							  A.COD_CFOP,
							  A.COD_FISCAL_ORIGEM,
							  K.ALIQUOTA_IMPOSTO AS ICMS_ALIQUOTA,
							  L.SIMBOLO_UNIDADE,
							  B.LX_INDICADOR_MERCADORIA,
							  A.LX_HASH,
							  I.UID_DOCUMENTO
						 FROM CAT_SKU A
							  INNER JOIN CAT_ARTIGO B
									  ON A.ID_ARTIGO = B.ID_ARTIGO
							  LEFT JOIN CAT_ARTIGO_VARIANTE_VALOR C
									 ON A.ID_ARTIGO_VARIANTE_VALOR_01 = C.ID_ARTIGO_VARIANTE_VALOR
							  LEFT JOIN CAT_ARTIGO_VARIANTE_VALOR D
									 ON A.ID_ARTIGO_VARIANTE_VALOR_02 = D.ID_ARTIGO_VARIANTE_VALOR
							  LEFT JOIN CAT_ARTIGO_VARIANTE_VALOR E
									 ON A.ID_ARTIGO_VARIANTE_VALOR_03 = E.ID_ARTIGO_VARIANTE_VALOR
							  LEFT JOIN CAT_ARTIGO_VARIANTE_VALOR F
									 ON A.ID_ARTIGO_VARIANTE_VALOR_04 = F.ID_ARTIGO_VARIANTE_VALOR
							  LEFT JOIN CAT_ARTIGO_VARIANTE_VALOR G
									 ON A.ID_ARTIGO_VARIANTE_VALOR_05 = G.ID_ARTIGO_VARIANTE_VALOR
							  LEFT JOIN CAT_MULTIMIDIA I
									 ON A.ID_SKU = I.ID_SKU
							  LEFT JOIN CAT_VW_REGRA_ICMS K
									 ON A.ID_SKU = K.ID_SKU
							  LEFT JOIN LJV_UNIDADE_MEDIDA L
									 ON B.ID_UNIDADE_MEDIDA = L.ID_UNIDADE_MEDIDA
						WHERE A.INATIVO = 0 
							  AND
							  B.INATIVO = 0;",
                "CREATE INDEX IDX_CAT_MULTIMIDIA_SKU ON CAT_MULTIMIDIA (ID_SKU );",
                "ALTER TABLE LJV_NOTA_FISCAL_SERIE ADD ID_TERMINAL INT NULL",
                "ALTER TABLE LJV_NOTA_FISCAL_SERIE ADD INATIVO BIT NOT NULL DEFAULT(0)",
                "UPDATE LJV_NOTA_FISCAL_SERIE SET INATIVO = 0 WHERE INATIVO IS NULL",
                "ALTER TABLE LJV_ATENDIMENTO_DOC_FISCAL ADD PROTOCOLO_DENEGACAO VARCHAR(30) NULL",
                "ALTER TABLE LJV_ATENDIMENTO_DOC_FISCAL ADD DATA_HORA_DENEGACAO DATETIME NULL",
                "ALTER TABLE LJV_ATENDIMENTO_DOC_FISCAL ADD PROTOCOLO_INUTILIZACAO VARCHAR(30) NULL",
                "ALTER TABLE LJV_ATENDIMENTO_DOC_FISCAL ADD DATA_HORA_INUTILIZACAO DATETIME NULL",
                "ALTER TABLE LJV_ATENDIMENTO_ITEM ADD DOCUMENTO_ORIGEM VARCHAR(100) NULL",

                @"CREATE TABLE IF NOT EXISTS LJV_DADOS_OFFLINE ( 
					ID_DADOS_OFFLINE    INTEGER PRIMARY KEY NOT NULL,
					LX_TIPO_DADO_OFFLINE TINYINT        NOT NULL,
					LX_TIPO_DOCUMENTO    TINYINT        NOT NULL,
					IDENTIFICADOR        VARCHAR(30)    NOT NULL,
					JSON                 TEXT,
					URL_WEBAPI           VARCHAR(150),     
					DATA_ENTRADA         DATE           NOT NULL,
					DATA_SAIDA           DATE,
					RETORNO_ENVIO        TEXT 
				);",
                @"
				CREATE TABLE IF NOT EXISTS LJV_FIDELIDADE_PROGRAMA_MENSAGEM ( 
					ID_FID_PROGRAMA_MENSAGEM INT             PRIMARY KEY NOT NULL,
					ID_FID_PROGRAMA          INT             NOT NULL,
					MENSAGEM                 VARCHAR( 150 ),
					LX_TIPO_MENSAGEM         TYNINT,
					FOREIGN KEY ( ID_FID_PROGRAMA ) REFERENCES LJV_FIDELIDADE_PROGRAMA ( ID_FIDELIDADE_PROGRAMA ) 
				); ",
                "ALTER TABLE LJV_ATENDIMENTO_DOC_FISCAL ADD LX_STATUS_NF TINYINT DEFAULT(1)",
                "ALTER TABLE LJV_PEDIDO_ITEM_VENDEDOR ADD ID_LINX INT NOT NULL DEFAULT(0)",
                @"
				CREATE TABLE IF NOT EXISTS LJV_CREDENCIADORA_CARTAO(
					   ID_CREDENCIADORA_CARTAO int NOT NULL,
					   COD_CREDENCIADORA_CARTAO varchar(3) NOT NULL,
					   RAZAO_SOCIAL_CREDENCIADORA_CARTAO varchar(90) NOT NULL,
					   CNPJ varchar(14) NOT NULL,
					CONSTRAINT XPKLJV_CREDENCIADORA_CARTAO PRIMARY KEY (ID_CREDENCIADORA_CARTAO)
				)",
                "ALTER TABLE LJV_ADM_CARTAO ADD COLUMN ID_CREDENCIADORA_CARTAO int REFERENCES LJV_CREDENCIADORA_CARTAO(ID_CREDENCIADORA_CARTAO)",
                @"
				CREATE TABLE IF NOT EXISTS LJV_STATUS_SEFAZ ( 
					COD_STATUS_SEFAZ   VARCHAR( 5 ) NOT NULL,
					DESCR_STATUS_SEFAZ VARCHAR( 200 ) NOT NULL,
					AUTORIZACAO        VARCHAR( 3 ) NOT NULL,
					CANC_INUT          VARCHAR( 3 ) NOT NULL,
					CONSULTA           VARCHAR( 3 ) NOT NULL,
					CONSTRAINT XPKLJV_STATUS_SEFAZ PRIMARY KEY (COD_STATUS_SEFAZ)
				);",
                @"
				DROP TABLE IF EXISTS LJV_SALE_WORKING;",
                @"
				DROP TRIGGER IF EXISTS TRG_ON_INSERT_LJV_ATENDIMENTO;",
                @"
				DROP TRIGGER IF EXISTS TRG_ON_UPDATE_LJV_ATENDIMENTO;",
                @"
				DROP TRIGGER IF EXISTS TRG_ON_INSERT_LJV_PEDIDO;",
                @"
				DROP TRIGGER IF EXISTS TRG_ON_UPDATE_LJV_PEDIDO;",
                @"
				DROP TRIGGER IF EXISTS TRG_ON_UPDATE_LJV_PEDIDO_1;",
                "ALTER TABLE CAT_SKU ADD COD_CEST VARCHAR(20) NOT NULL DEFAULT ('')",
                "ALTER TABLE CAT_SKU_CODIGO_BARRA ADD LX_TIPO_CODIGO_BARRA SMALLINT NULL ",
                "ALTER TABLE CAT_SKU_CODIGO_BARRA ADD INDICA_GTIN_FISCAL BIT NOT NULL DEFAULT(0)",
                @"ALTER TABLE LJV_LOJA ADD CODIGO_REGIME_TRIBUTARIO_NFE VARCHAR(5) NULL",
                @"ALTER TABLE LJV_LOJA DROP COD_REGIME_TRIBUTARIO_NFE",
                @"ALTER TABLE LJV_PEDIDO ADD DOC_CLIENTE VARCHAR(19) NULL",
                @"DROP VIEW VW_LISTA_PEDIDO; ",
                @"CREATE VIEW VW_LISTA_PEDIDO AS 
						SELECT  ID_PEDIDO, 
								LX_STATUS_PEDIDO, 
								LX_TIPO_PEDIDO,
								NUMERO_PEDIDO, 
								IDENTIFICACAO_PEDIDO, 
								NOME_CLIENTE_VAREJO, 
								DATA_PEDIDO, 
								QTDE_LIQUIDA_TOTAL, 
								VALOR_TOTAL_BRUTO, 
								VALOR_TOTAL_LIQUIDO, 
								VALOR_DESCONTO,
                                ID_CRM_PFJ,
                                CPF_CNPJ,
								DOC_CLIENTE,                                
								IFNULL((SELECT 1 FROM LJV_PEDIDO_VENDEDOR WHERE ID_PEDIDO = A.ID_PEDIDO AND ID_VENDEDOR = -1),0) AS ESCOLHE_VENDEDOR
						FROM LJV_PEDIDO A",
                @"
				CREATE TABLE IF NOT EXISTS LJV_ATENDIMENTO_ATRIBUTO (
					ID_ATENDIMENTO_ATRIBUTO VARCHAR (40)  NOT NULL,
					ID_ATENDIMENTO          VARCHAR (40)  NOT NULL,
					ID_LINX                 INT           NOT NULL,
					LX_TIPO_VALOR           TINYINT       DEFAULT 1
														  NOT NULL,
					VALOR_ATRIBUTO          VARCHAR (250) DEFAULT ''
														  NOT NULL,
					CONSTRAINT XPK_LJV_ATENDIMENTO_ATRIBUTO PRIMARY KEY (
						ID_ATENDIMENTO_ATRIBUTO
					),
					CONSTRAINT XFK_LJV_ATENDIMENTO_ATRIBUTO_1 FOREIGN KEY (
						ID_ATENDIMENTO
					)
					REFERENCES LJV_ATENDIMENTO (ID_ATENDIMENTO) ON DELETE CASCADE
				)",
                @"DROP VIEW IF EXISTS VW_CONSULTA_VENDAS_ATENDIMENTOS",
                @"
				CREATE VIEW IF NOT EXISTS VW_CONSULTA_VENDAS_ATENDIMENTOS AS 
                        SELECT A.ID_ATENDIMENTO,
                               A.NUMERO_ATENDIMENTO,
                               A.LX_STATUS_ATENDIMENTO,
                               A.LX_TIPO_ATENDIMENTO,
                               A.DATA_ATENDIMENTO,
                               A.QTDE_MERCADORIA_SERVICO,
                               A.VALOR_DESCONTO_SUBTOTAL,     
                               A.VALOR_DESCONTO_ITENS,  
                               ifNull(vDesc.TOTAL_DESCONTO_ITENS, 0) TOTAL_DESCONTO_ITENS,
                               ifNull(vDesc.TOTAL_ACRESCIMO_ITENS, 0) TOTAL_ACRESCIMO_ITENS,
                               A.VALOR_MERCADORIA_SERVICO,
                               A.VALOR_ACRESCIMO_SUBTOTAL,
                               A.VALOR_ITENS_VALE_PRESENTE,
                               A.VALOR_IMPOSTO_AGREGADO,
                               A.VALOR_ITENS_RECEBIMENTO,
                               A.VALOR_ITENS_PEDIDO,
                               A.VALOR_TOTAL,
                               A.HORA_INICIO,
                               A.HORA_FIM,
                               A.ID_CRM_PFJ,
                               A.CPF_CNPJ,
                               A.NOME_CLIENTE_VAREJO,
                               B.ID_TERMINAL,
                               V.NOME_VENDEDOR,
                               G.NOME_VENDEDOR AS 'NOME_GERENTE',
                               C.COD_TERMINAL,
                               D.COO,
                               E.NUMERO_DOCUMENTO,
                               (CASE A.VALOR_ITENS_VALE_PRESENTE
                                    WHEN 0 THEN 0
                                    ELSE
                                           (SELECT COUNT(0)
                                            FROM LJV_ATENDIMENTO_ITEM ITEM
                                            WHERE ITEM.ID_ATENDIMENTO = A.ID_ATENDIMENTO
                                              AND ITEM.LX_TIPO_ITEM = 4
                                              AND ITEM.ID_CAIXA_RECEBIMENTO IS NOT NULL)
                                END) QTD_VALE_PRESENTE,
                               U.DESC_OPERACAO AS OPERACAO_VENDA
                        FROM LJV_ATENDIMENTO A
                        INNER JOIN LJV_CAIXA_CTRL B ON B.ID_CAIXA_CTRL = A.ID_CAIXA_CTRL
                        INNER JOIN LJV_TERMINAL C ON (B.ID_TERMINAL = C.ID_TERMINAL
                                                      AND C.ID_LOJA = A.ID_LOJA)
                        LEFT JOIN LJV_ECF_OPERACAO D ON A.ID_ECF_OPERACAO = D.ID_ECF_OPERACAO
                        LEFT JOIN LJV_ATENDIMENTO_DOC_FISCAL E ON (A.ID_ATENDIMENTO = E.ID_ATENDIMENTO
                                                                   AND E.LX_STATUS_NF = 2)
                        LEFT JOIN LJV_VENDEDOR G ON B.ID_GERENTE_PERIODO = G.ID_VENDEDOR
                        LEFT JOIN LJV_VENDEDOR V ON B.ID_OPERADOR_CAIXA = V.ID_VENDEDOR
                        INNER JOIN LJV_OPERACAO_VENDA U ON A.ID_OPERACAO_VENDA = U.ID_OPERACAO_VENDA
                        INNER JOIN (    
                        SELECT  ID_ATENDIMENTO,     
                                SUM(CASE WHEN DESCONTO_ITEM < 0 THEN (DESCONTO_ITEM * QTDE_ITEM) * -1 END) AS TOTAL_ACRESCIMO_ITENS,
                                SUM(CASE WHEN DESCONTO_ITEM > 0 THEN (DESCONTO_ITEM * QTDE_ITEM) END) TOTAL_DESCONTO_ITENS
                          FROM LJV_ATENDIMENTO_ITEM 
                        WHERE ITEM_CANCELADO = 0 
                        GROUP BY ID_ATENDIMENTO
                        ) vDesc
                        ON A.ID_ATENDIMENTO = vDesc.ID_ATENDIMENTO
                        WHERE A.ATENDIMENTO_CANCELADO = 0
                          AND A.LX_STATUS_ATENDIMENTO = 3

                    ",
                @"DROP VIEW IF EXISTS VW_CONSULTA_VENDAS_ITENS",
                @"
				CREATE VIEW IF NOT EXISTS VW_CONSULTA_VENDAS_ITENS AS
					SELECT ID_ATENDIMENTO_ITEM,
						   B.ID_ATENDIMENTO,
						   B.ID_SKU,
						   B.LX_TIPO_ITEM,
						   B.QTDE_ITEM,
						   B.PRECO_BRUTO_ITEM,
						   B.DESCONTO_ITEM,
						   B.VALOR_ITEM,
						   B.ITEM_CANCELADO,
						   B.PORCENTAGEM_ITEM_RATEIO,
						   B.VALOR_DESCONTO_SUBTOTAL_RATEIO,
						   B.VALOR_ACRESCIMO_SUBTOTAL_RATEIO,
						   B.VALOR_LIQUIDO_PAGO,
						   A.DATA_ATENDIMENTO,
						   A.LX_TIPO_ATENDIMENTO,
						   C.ID_TERMINAL,
						   D.DESC_SKU
					 FROM LJV_ATENDIMENTO A, 
						  LJV_ATENDIMENTO_ITEM B, 
						  LJV_CAIXA_CTRL C, 
						  CAT_SKU D
					WHERE A.ID_ATENDIMENTO = B.ID_ATENDIMENTO 
						AND C.ID_CAIXA_CTRL = A.ID_CAIXA_CTRL 
						AND D.ID_SKU = B.ID_SKU                        
						AND A.LX_STATUS_ATENDIMENTO = 3 
						AND A.ATENDIMENTO_CANCELADO = 0 
						AND B.ITEM_CANCELADO = 0",
                 @"ALTER TABLE CAT_SKU ADD COD_ITEM_FISCAL VARCHAR(60) NULL",
                 @"ALTER TABLE CAT_SKU ADD DESC_ITEM_FISCAL VARCHAR(120) NULL",
                 @"ALTER TABLE LJ_ETL_REPOSITORIO ADD INDICA_ENVIADO_MASTER BIT(1) DEFAULT 0",

                 @"DROP VIEW VW_LJV_ATENDIMENTO_VENDEDOR",
                 @"CREATE VIEW VW_LJV_ATENDIMENTO_VENDEDOR AS 
				  SELECT VA.ID_ATENDIMENTO, VA.ID_VENDEDOR
				  FROM LJV_ATENDIMENTO_VENDEDOR VA
					  LEFT JOIN LJV_ATENDIMENTO_ITEM_VENDEDOR VI
						ON VA.ID_ATENDIMENTO_VENDEDOR=VI.ID_ATENDIMENTO_VENDEDOR 
				  WHERE VI.ID_ATENDIMENTO_VENDEDOR IS NULL",

                 @"DROP VIEW VW_LJV_ATENDIMENTO_ITEM_VENDEDOR",
                 @"CREATE VIEW VW_LJV_ATENDIMENTO_ITEM_VENDEDOR AS
				   SELECT VA.ID_ATENDIMENTO, VI.ID_ATENDIMENTO_ITEM, VA.ID_VENDEDOR
				   FROM LJV_ATENDIMENTO_VENDEDOR VA
					  JOIN LJV_ATENDIMENTO_ITEM_VENDEDOR VI
						  ON VA.ID_ATENDIMENTO_VENDEDOR=VI.ID_ATENDIMENTO_VENDEDOR",

                 @"INSERT INTO LJV_ATENDIMENTO_VENDEDOR (ID_ATENDIMENTO_VENDEDOR, ID_ATENDIMENTO, ID_VENDEDOR, ID_LINX)
					SELECT C.ID_ATENDIMENTO_ITEM, D.ID_ATENDIMENTO, D.ID_VENDEDOR, D.ID_LINX
					FROM (
						SELECT VA.ID_ATENDIMENTO, MIN(SEQ_ITEM) AS SEQ_ITEM
						FROM LJV_ATENDIMENTO_VENDEDOR VA
							JOIN LJV_ATENDIMENTO_ITEM_VENDEDOR VI
							   ON VA.ID_ATENDIMENTO_VENDEDOR=VI.ID_ATENDIMENTO_VENDEDOR
							JOIN LJV_ATENDIMENTO_ITEM AI
							   ON AI.ID_ATENDIMENTO_ITEM=VI.ID_ATENDIMENTO_ITEM AND VA.ID_ATENDIMENTO=AI.ID_ATENDIMENTO
							LEFT JOIN VW_LJV_ATENDIMENTO_VENDEDOR VW ON VW.ID_ATENDIMENTO=VA.ID_ATENDIMENTO
						 WHERE VW.ID_ATENDIMENTO IS NULL
						 GROUP BY VA.ID_ATENDIMENTO 
						) A
						JOIN LJV_ATENDIMENTO_ITEM B ON B.ID_ATENDIMENTO=A.ID_ATENDIMENTO AND B.SEQ_ITEM=A.SEQ_ITEM
						JOIN LJV_ATENDIMENTO_ITEM_VENDEDOR C ON C.ID_ATENDIMENTO_ITEM=B.ID_ATENDIMENTO_ITEM
						JOIN LJV_ATENDIMENTO_VENDEDOR D ON D.ID_ATENDIMENTO_VENDEDOR=C.ID_ATENDIMENTO_VENDEDOR",
                 @"CREATE TABLE IF NOT EXISTS LJV_MODULO (
					ID_MODULO integer,
					DESC_MODULO nvarchar(60) NOT NULL,
					INATIVO bit NOT NULL DEFAULT ((0)),
					ICONE nvarchar(40),
					LX_COR_FUNDO int,
					LX_TAMANHO_APRESENTACAO int,
					ORDEM_NAVEGACAO tinyint NOT NULL DEFAULT ((1)),
					ID_TCS_APLICATIVO int NOT NULL,
					CONSTRAINT XPK_LJV_MODULO PRIMARY KEY (ID_MODULO)
				 );",
                 @"CREATE TABLE IF NOT EXISTS LJV_MODULO_MENU (
					ID_MODULO_MENU integer,
					ID_MODULO_MENU_SUPERIOR integer,
					ID_MODULO integer NOT NULL,
					DESC_MODULO_MENU nvarchar(60) NOT NULL,
					ORDEM_NAVEGACAO tinyint NOT NULL DEFAULT ((1)),
					ICONE nvarchar(40),
					LX_COR_FUNDO int,
					LX_TAMANHO_APRESENTACAO int,
					CONSTRAINT XPK_LJV_MODULO_MENU PRIMARY KEY (ID_MODULO_MENU),
					FOREIGN KEY (ID_MODULO_MENU_SUPERIOR) REFERENCES LJV_MODULO_MENU (ID_MODULO_MENU),
					FOREIGN KEY (ID_MODULO) REFERENCES LJV_MODULO (ID_MODULO)
				 );",
                 @"CREATE TABLE IF NOT EXISTS LJV_TRANSACAO_MENU (
					ID_TRANSACAO_MENU integer PRIMARY KEY AUTOINCREMENT ,
					ID_MODULO_MENU integer NOT NULL,
					ID_TRANSACAO integer NOT NULL,
					ORDEM_NAVEGACAO tinyint NOT NULL DEFAULT ((1)),
					INATIVO bit NOT NULL DEFAULT ((0)),
					FOREIGN KEY (ID_TRANSACAO) REFERENCES LJV_TRANSACAO (ID_TRANSACAO),
					FOREIGN KEY (ID_MODULO_MENU) REFERENCES LJV_MODULO_MENU (ID_MODULO_MENU)
				 );",
                 @"CREATE TABLE IF NOT EXISTS LJV_TRANSACAO (
					ID_TRANSACAO integer,
					COD_TRANSACAO nvarchar(10) NOT NULL,
					DESC_TRANSACAO nvarchar(60) NOT NULL,
					LX_TIPO_TRANSACAO tinyint NOT NULL,
					CLASSE_NOME nvarchar(400) NOT NULL,
					INATIVO bit NOT NULL DEFAULT ((0)),
					ICONE nvarchar(40),
					LX_COR_FUNDO int,
					LX_TAMANHO_APRESENTACAO int,
					CONSTRAINT XPK_LJV_TRANSACAO PRIMARY KEY (ID_TRANSACAO)
				 );",
                 "CREATE INDEX LJV_MODULO_MENU_IX_ID_MODULO_MENU_SUPERIOR ON LJV_MODULO_MENU(ID_MODULO_MENU_SUPERIOR);",
                 "CREATE INDEX LJV_MODULO_MENU_IX_ID_MODULO ON LJV_MODULO_MENU(ID_MODULO);",
                 "CREATE INDEX LJV_TRANSACAO_MENU_IX_ID_MODULO_MENU ON LJV_TRANSACAO_MENU(ID_MODULO_MENU);",
                 "CREATE INDEX LJV_TRANSACAO_MENU_IX_ID_TRANSACAO ON LJV_TRANSACAO_MENU(ID_TRANSACAO);",
                "INSERT INTO LJV_MODULO (ID_MODULO, DESC_MODULO, INATIVO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO, ORDEM_NAVEGACAO, ID_TCS_APLICATIVO) VALUES (320000, 'Metas e Comissões', 0, NULL, 2, 2, 0, 5);",
                "INSERT INTO LJV_MODULO (ID_MODULO, DESC_MODULO, INATIVO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO, ORDEM_NAVEGACAO, ID_TCS_APLICATIVO) VALUES (330000, 'Promoções e Ofertas', 0, NULL, 2, 2, 0, 5);",
                "INSERT INTO LJV_MODULO (ID_MODULO, DESC_MODULO, INATIVO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO, ORDEM_NAVEGACAO, ID_TCS_APLICATIVO) VALUES (340000, 'Analytics', 0, NULL, 5, 1, 0, 5);",
                "INSERT INTO LJV_MODULO (ID_MODULO, DESC_MODULO, INATIVO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO, ORDEM_NAVEGACAO, ID_TCS_APLICATIVO) VALUES (350000, 'Atendimentos Vendas', 0, NULL, 2, 2, 0, 5);",
                "INSERT INTO LJV_MODULO_MENU (ID_MODULO_MENU, ID_MODULO_MENU_SUPERIOR, ID_MODULO, DESC_MODULO_MENU, ORDEM_NAVEGACAO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (1870000, NULL, 320000, 'Metas', 0, NULL, 2, 2);",
                "INSERT INTO LJV_MODULO_MENU (ID_MODULO_MENU, ID_MODULO_MENU_SUPERIOR, ID_MODULO, DESC_MODULO_MENU, ORDEM_NAVEGACAO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (1880000, NULL, 320000, 'Comissões', 0, NULL, 2, 2);",
                "INSERT INTO LJV_MODULO_MENU (ID_MODULO_MENU, ID_MODULO_MENU_SUPERIOR, ID_MODULO, DESC_MODULO_MENU, ORDEM_NAVEGACAO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (1890000, 1870000, 320000, 'Consultas', 0, '', 2, 2);",
                "INSERT INTO LJV_MODULO_MENU (ID_MODULO_MENU, ID_MODULO_MENU_SUPERIOR, ID_MODULO, DESC_MODULO_MENU, ORDEM_NAVEGACAO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (1900000, 1870000, 320000, 'Manutenção', 0, '', 2, 2);",
                "INSERT INTO LJV_MODULO_MENU (ID_MODULO_MENU, ID_MODULO_MENU_SUPERIOR, ID_MODULO, DESC_MODULO_MENU, ORDEM_NAVEGACAO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (1910000, NULL, 330000, 'Manutenção', 0, NULL, 2, 2);",
                "INSERT INTO LJV_MODULO_MENU (ID_MODULO_MENU, ID_MODULO_MENU_SUPERIOR, ID_MODULO, DESC_MODULO_MENU, ORDEM_NAVEGACAO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (1920000, NULL, 340000, 'Estoque', 0, 'fa-cubes', 2, 2);",
                "INSERT INTO LJV_MODULO_MENU (ID_MODULO_MENU, ID_MODULO_MENU_SUPERIOR, ID_MODULO, DESC_MODULO_MENU, ORDEM_NAVEGACAO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (1930000, NULL, 350000, 'Manutenção', 0, NULL, 2, 2);",
                "INSERT INTO LJV_TRANSACAO (ID_TRANSACAO, COD_TRANSACAO, DESC_TRANSACAO, LX_TIPO_TRANSACAO, CLASSE_NOME, INATIVO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (9740000, 'UI0228', 'Distribuição de Meta por Loja', 2, 'linx-operacional-loja-meta-bv-spa-distribuicaometaloja', 0, NULL, 3, 2);",
                "INSERT INTO LJV_TRANSACAO (ID_TRANSACAO, COD_TRANSACAO, DESC_TRANSACAO, LX_TIPO_TRANSACAO, CLASSE_NOME, INATIVO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (10860000, 'UI0091', 'Manutenção de Atendimento', 2, 'linx-operacional-loja-atendimento-bv-spa-atendimento', 0, NULL, 3, 2);",
                "INSERT INTO LJV_TRANSACAO (ID_TRANSACAO, COD_TRANSACAO, DESC_TRANSACAO, LX_TIPO_TRANSACAO, CLASSE_NOME, INATIVO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (11070000, 'UI0229', 'Distribuição de Meta por Vendedor', 2, 'linx-operacional-loja-meta-bv-spa-distribuicaometalojavendedor', 0, NULL, 3, 2);",
                "INSERT INTO LJV_TRANSACAO (ID_TRANSACAO, COD_TRANSACAO, DESC_TRANSACAO, LX_TIPO_TRANSACAO, CLASSE_NOME, INATIVO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (12880000, 'UI0139', 'Cadastro Lista de Cupons/PinCodes', 2, 'linx-operacional-promocao-bv-spa-cadastrocupom', 0, NULL, 3, 2);",
                "INSERT INTO LJV_TRANSACAO (ID_TRANSACAO, COD_TRANSACAO, DESC_TRANSACAO, LX_TIPO_TRANSACAO, CLASSE_NOME, INATIVO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (13140000, 'UI0387', 'Consulta de Meta de Vendedor por Loja', 2, 'linx-operacional-loja-meta-bv-spa-consultametalojavendedor', 0, NULL, 3, 2);",
                "INSERT INTO LJV_TRANSACAO (ID_TRANSACAO, COD_TRANSACAO, DESC_TRANSACAO, LX_TIPO_TRANSACAO, CLASSE_NOME, INATIVO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (13410000, 'UI0087', 'Cadastro do Cenário de Meta', 2, 'linx-operacional-loja-meta-bv-spa-cenariometaloja', 0, NULL, 3, 2);",
                "INSERT INTO LJV_TRANSACAO (ID_TRANSACAO, COD_TRANSACAO, DESC_TRANSACAO, LX_TIPO_TRANSACAO, CLASSE_NOME, INATIVO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (13690000, 'UI0574', 'Consulta Posição de Estoque', 2, 'linx-operacional-estoquecons-bv-spa-consestoque', 0, NULL, 3, 2);",
                "INSERT INTO LJV_TRANSACAO (ID_TRANSACAO, COD_TRANSACAO, DESC_TRANSACAO, LX_TIPO_TRANSACAO, CLASSE_NOME, INATIVO, ICONE, LX_COR_FUNDO, LX_TAMANHO_APRESENTACAO) VALUES (14190000, 'UI0386', 'Consulta de Meta por Loja', 2, 'linx-operacional-loja-meta-bv-spa-consultametaloja', 0, NULL, 3, 2);",
                "INSERT INTO LJV_TRANSACAO_MENU (ID_TRANSACAO_MENU, ID_MODULO_MENU, ID_TRANSACAO, ORDEM_NAVEGACAO, INATIVO) VALUES (8110000, 1900000, 13410000, 0, 0);",
                "INSERT INTO LJV_TRANSACAO_MENU (ID_TRANSACAO_MENU, ID_MODULO_MENU, ID_TRANSACAO, ORDEM_NAVEGACAO, INATIVO) VALUES (8120000, 1900000, 9740000, 0, 0); ",
                "INSERT INTO LJV_TRANSACAO_MENU (ID_TRANSACAO_MENU, ID_MODULO_MENU, ID_TRANSACAO, ORDEM_NAVEGACAO, INATIVO) VALUES (8130000, 1900000, 11070000, 0, 0);",
                "INSERT INTO LJV_TRANSACAO_MENU (ID_TRANSACAO_MENU, ID_MODULO_MENU, ID_TRANSACAO, ORDEM_NAVEGACAO, INATIVO) VALUES (8170000, 1890000, 14190000, 0, 0);",
                "INSERT INTO LJV_TRANSACAO_MENU (ID_TRANSACAO_MENU, ID_MODULO_MENU, ID_TRANSACAO, ORDEM_NAVEGACAO, INATIVO) VALUES (8180000, 1890000, 13140000, 0, 0);",
                "INSERT INTO LJV_TRANSACAO_MENU (ID_TRANSACAO_MENU, ID_MODULO_MENU, ID_TRANSACAO, ORDEM_NAVEGACAO, INATIVO) VALUES (8220000, 1910000, 12880000, 0, 0);",
                "INSERT INTO LJV_TRANSACAO_MENU (ID_TRANSACAO_MENU, ID_MODULO_MENU, ID_TRANSACAO, ORDEM_NAVEGACAO, INATIVO) VALUES (8270000, 1920000, 13690000, 0, 0);",
                "INSERT INTO LJV_TRANSACAO_MENU (ID_TRANSACAO_MENU, ID_MODULO_MENU, ID_TRANSACAO, ORDEM_NAVEGACAO, INATIVO) VALUES (8280000, 1930000, 10860000, 0, 0);",
                "ALTER TABLE LJV_ATENDIMENTO_ITEM ADD COD_CEST VARCHAR(20) NULL;",
                 @"CREATE TABLE LJV_PAF_ECF_ESTOQUE ( 
					ID_SKU           INTEGER           PRIMARY KEY
														NOT NULL,
					COD_SKU          VARCHAR( 25 )     NOT NULL,
					DESC_SKU         VARCHAR( 120 )    NOT NULL,
					COD_ITEM_FISCAL  VARCHAR( 60 ),
					DESC_ITEM_FISCAL VARCHAR( 120 ),
					STK_SALDO_QTDE   NUMERIC( 15, 3 )  DEFAULT ( 0 ),
					DATA_INICIAL_REF DATETIME          NOT NULL,
					DATA_FINAL_REF   DATETIME          NOT NULL,
					FOREIGN KEY ( ID_SKU ) REFERENCES CAT_SKU ( ID_SKU ) ON DELETE CASCADE
																				ON UPDATE CASCADE 
				);",
                @"
				CREATE TABLE IF NOT EXISTS LJV_PAF_ECF_ATRIBUTO 
				( 
					ID_PAF_ECF_ATRIBUTO VARCHAR (40) PRIMARY KEY NOT NULL, 
					DESCRICAO_ATRIBUTO VARCHAR (250) NOT NULL, 
					TIPO_VALOR VARCHAR (100) NOT NULL, 
					VALOR_ATRIBUTO VARCHAR NOT NULL
				)",
                @"ALTER TABLE LJV_PEDIDO_ITEM ADD ID_SKU_SOLICITADO INT NULL;",
                @"ALTER TABLE LJV_DADOS_OFFLINE ADD LX_TIPO_DOCUMENTO TINYINT NULL;",
                @"ALTER TABLE LJV_TERMINAL ADD IP_TERMINAL      VARCHAR( 40 );",
                @"
                CREATE TABLE IF NOT EXISTS LJV_ATENDIMENTO_PEDIDO ( 
                        ID_ATENDIMENTO_PEDIDO varchar (40) NOT NULL, 
                        ID_ATENDIMENTO varchar (40) NOT NULL, 
                        ID_PEDIDO varchar(40) NOT NULL, 
                        NUMERO_PEDIDO varchar(20) null,
                        TERMINAL_ORIGEM VARCHAR(40) NULL,
                CONSTRAINT XPK_LJV_ATENDIMENTO_PEDIDO
                    PRIMARY KEY (ID_ATENDIMENTO_PEDIDO), 
                        CONSTRAINT XFK_LJV_ATENDIMENTO_ATRIBUTO_1 
                            FOREIGN KEY (ID_ATENDIMENTO) REFERENCES LJV_ATENDIMENTO (ID_ATENDIMENTO) ON DELETE CASCADE);",
                @"ALTER TABLE LJV_ATENDIMENTO_ITEM ADD SEQ_KIT SMALLINT NULL;",
                @"ALTER TABLE LJV_PEDIDO_ITEM ADD SEQ_KIT SMALLINT NULL;",
                @"ALTER TABLE LJV_ATENDIMENTO ADD LX_TIPO_FISCAL tinyint NOT NULL DEFAULT(99);",
                @"CREATE TABLE IF NOT EXISTS LJV_OPERACAO_VENDA_TIPO_CLIENTE(
                    ID_OPERACAO_VENDA_TIPO_CLIENTE      int              NOT NULL,
                    ID_OPERACAO_VENDA                                 int              NOT NULL,
                    ID_CRM_TIPO_CLIENTE                               int              NOT NULL,
                    CONSTRAINT XPKLJV_OPERACAO_VENDA PRIMARY KEY (ID_OPERACAO_VENDA_TIPO_CLIENTE),
                        CONSTRAINT FK_LJV_OPERACAO_VENDA_E0070B7F FOREIGN KEY (ID_OPERACAO_VENDA) REFERENCES LJV_OPERACAO_VENDA(ID_OPERACAO_VENDA), 
                        CONSTRAINT FK_LJV_OPERACAO_VENDA_9D2D9448 FOREIGN KEY (ID_CRM_TIPO_CLIENTE) REFERENCES CRM_TIPO_CLIENTE(ID_CRM_TIPO_CLIENTE)
                )",
                @"ALTER TABLE LJV_ATENDIMENTO_FIDELIDADE ADD INDICA_PROCESSADO BIT NOT NULL DEFAULT(1);",
                @"CREATE TABLE IF NOT EXISTS LJV_REPOSITORIO_DOCUMENTO (
                        ID_DOCUMENTO VARCHAR (40) PRIMARY KEY NOT NULL, 
                        ID_CAIXA_CTRL VARCHAR (40) NOT NULL,
                        ID_ECF_OPERACAO VARCHAR (40), 
                        ID_ATENDIMENTO VARCHAR (40), 
                        LX_TIPO_DOCUMENTO smallint, 
                        CONTEUDO_DOCUMENTO TEXT,
                        DIRETORIO_DOCUMENTO varchar(500)
                );",
                @"ALTER TABLE LJV_ATENDIMENTO_DOC_FISCAL ADD ID_OPERACAO_FINALIDADE_NOTA_FISCAL int null;",
                @"ALTER TABLE LJV_DATABASE ADD ULTIMO_ID_REQUISICAO VARCHAR(40)",
                @"ALTER TABLE LJV_DATABASE ADD REQUISICAO_RECEBIDA BIT",
                @"ALTER TABLE LJV_ATENDIMENTO ADD DATA_HORA_ALTERACAO DATETIME NULL",
                @"ALTER TABLE LJV_CAIXA_CTRL ADD MOBILE BIT NOT NULL DEFAULT(0)",
                @"ALTER TABLE LJV_PEDIDO ADD ID_MOTIVO_CANCELAMENTO smallint NULL",
                @"ALTER TABLE LJV_PEDIDO ADD ID_MOTIVO_DESCONTO smallint NULL",
                @"ALTER TABLE LJV_PEDIDO_ITEM ADD ID_MOTIVO_CANCELAMENTO smallint NULL",
                @"ALTER TABLE LJV_PEDIDO_ITEM ADD ID_MOTIVO_DESCONTO smallint NULL",
                @"ALTER TABLE LJV_PEDIDO_ITEM ADD LX_TIPO_ATENDIMENTO smallint DEFAULT 1 NOT NULL",
                @"ALTER TABLE LJV_PEDIDO_ITEM ADD COO_ORIGEM smallint",
                @"ALTER TABLE LJV_PEDIDO_ITEM ADD DOCUMENTO_ORIGEM VARCHAR(100)",
                @"CREATE TABLE IF NOT EXISTS HISTORICO_VERSOES(
                                                    ID                         INT      DEFAULT 0 NOT NULL,
                                                    TIPO                       TINYINT  DEFAULT 1 NOT NULL,
                                                    MAJOR                      SMALLINT DEFAULT 0 NOT NULL,
                                                    MINOR                      SMALLINT DEFAULT 0 NOT NULL,
                                                    BUILD                      SMALLINT DEFAULT 0 NOT NULL,
                                                    REVISION                   SMALLINT DEFAULT 0 NOT NULL,
                                                    PRIMEIRA_EXECUCAO_SISTEMA  DATE     DEFAULT (DATETIME('NOW')) NOT NULL,
                                                    ULTIMA_EXECUCAO_SISTEMA    DATE     DEFAULT (DATETIME('NOW')) NOT NULL,
                                                    CONSTRAINT PK_HISTORICO_VERSOES PRIMARY KEY (ID)
                                                );"
            };

            SQLiteConnection cn;
            SQLiteCommand cmd = new SQLiteCommand();
            cn = CreateConnection(path);
            cn.Open();

            foreach (string command in sourceModel)
            {
                try
                {
                    cmd.CommandText = command;
                    cmd.ExecuteNonQuery();
                }
                catch { }
            }
            cn.Close();

            return true;
        }

        public bool TransferTable(string originPath, string destinationPath, string table, TransferType type)
        {
            try
            {
                SQLiteConnection cn1;
                SQLiteDataAdapter db1;
                DataSet ds1 = new DataSet();
                DataTable dt1 = new DataTable();

                SQLiteConnection cn2;
                
                cn1 = CreateConnection(originPath);
                cn1.Open();

                string sql = $"SELECT * FROM {table}";

                db1 = new SQLiteDataAdapter(sql, cn1);
                ds1.Reset();
                db1.Fill(ds1);
                dt1 = ds1.Tables[0];

                string colums = "";

                List<string> values = new List<string>();

                foreach (var x in dt1.Columns)
                {
                    colums += $"{x},";
                }

                colums = colums.Substring(0, colums.Length - 1);

                int col = 0;
                for (int i =0; i < dt1.Rows.Count; i++)
                {
                    string val = "";
                    foreach (var x in dt1.Rows[i].ItemArray)
                    {
                        if (x.GetType() != typeof(System.DBNull))
                        {
                            var tipo = dt1.Columns[col].DataType.FullName;
                            if (tipo == "System.String") 
                                val += $"'{x.ToString().Replace("'","''")}',";
                            else if (tipo == "System.DateTime")
                                val += $"'{((DateTime)x).ToString("yyyy-MM-dd HH:mm:ss")}',";
                            else if(tipo == "System.Boolean")
                                val += $"{(x.ToString() == "True" ? 1 : 0)},";
                            else if (tipo == "System.Decimal")
                                val += $"{x.ToString().Replace(",",".")},";
                            else
                                val += $"{x},";
                        }
                        else
                            val += "NULL,";

                        col++;
                    }

                    col = 0;
                    val = val.Substring(0, val.Length - 1);

                    values.Add(val);
                }

                cn1.Close();

                cn2 = CreateConnection(destinationPath);
                cn2.Open();

                SQLiteCommand command = new SQLiteCommand();
                command.Connection = cn2;

                command.CommandText = "PRAGMA foreign_keys=OFF;";
                command.ExecuteNonQuery();

                if (type == TransferType.Replace)
                {
                    command.CommandText = $"DELETE FROM {table}";
                    command.ExecuteNonQuery();
                }
                
                int progress = 0;
                int total = values.Count;
                foreach (var v in values)
                {
                    try
                    {
                        command.CommandText = $"INSERT INTO {table} ({colums}) VALUES ({v})";
                        command.ExecuteNonQuery();
                        progress++;

                        OnInsertProgress(new Core.InsertProgressEventArgs(progress, total));

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                command.CommandText = "PRAGMA foreign_keys=ON;";
                command.ExecuteNonQuery();

                cn2.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public String CreateNewDataBase()
        {
            return "";
        }
    }
}
