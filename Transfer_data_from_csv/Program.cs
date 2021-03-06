﻿using System;
using System.Collections.Generic;
using Transfer_data_from_csv.Services;
using Transfer_data_from_csv.Entities;
using Transfer_data_from_csv.Helpers;
using System.Threading.Tasks;

namespace Transfer_data_from_csv
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Reading from file
            ReadFromFile CsvFile = new ReadFromFile();
            var entities = CsvFile.FileReading();
            var dataService = new AnswerDataService();
            try
            {
                await dataService.InsertData(entities, ConstantHelper.accountName, ConstantHelper.accountKey, ConstantHelper.firstTableName);
            }
            catch (Exception ex)
            {
                var v = ex;
            }

            //Reading from Table
            DataFromTableService tableData = new DataFromTableService();
            var DataTable = new List<AnswerEntities>();
            try
            {
                DataTable = await tableData.OutputData<AnswerEntities>(ConstantHelper.accountName, ConstantHelper.accountKey,ConstantHelper.firstTableName);
            }
            catch (Exception ex)
            {
                var v = ex;
            }

            var templInsert = new InsertAnalizedDatas();
            var processedData = templInsert.InserTAfterAnalized(DataTable);

            try
            {
                await dataService.InsertData(processedData, ConstantHelper.accountName, ConstantHelper.accountKey, ConstantHelper.secondTableName);
            }
            catch (Exception ex)
            {
                var v = ex;
            }

            //write to csv file data from table FinnalyData

            var DataToCSV = new List<AnalyzedDatasEntities>();
            try
            {
                DataToCSV = await tableData.OutputData<AnalyzedDatasEntities>(ConstantHelper.accountName, ConstantHelper.accountKey, ConstantHelper.secondTableName);
            }
            catch (Exception ex)
            {
                var v = ex;
            }

            var FileWriter = new WriteToFile();
            FileWriter.WriteToCSV(DataToCSV);
        Console.WriteLine("Press any key");
            Console.ReadKey();
        }

    }

}


