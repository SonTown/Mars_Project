using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OfficeOpenXml;
using Structs;
using EnumTypes;
using System;

public class DataManager : BaseMonoBehaviour
{
    private CraftUIinfo[] craftUIinfos;
    private CraftUIinfo craftUIinfo;
    private IGameManager _gameManager;
    public Dictionary<CraftTypes, Dataset_craft> craftRequests = new Dictionary<CraftTypes, Dataset_craft>();
    private CraftTypes crafttype;
    public void Start()
    {

    }
    public IGameManager GameManager
    {
        get { return _gameManager; }
    }

    public void Init(IGameManager gameManager)
    {
        Debug.Log("DataManager Ready");
        _gameManager = gameManager;
    }
    public void Excelread(string Filename)
    {
        // Excel 파일 경로
        string excelFilePath = Filename;

        // Excel 파일 로드
        FileInfo fileInfo = new FileInfo(excelFilePath);
        using (var package = new ExcelPackage(fileInfo))
        {
            // 원하는 시트 선택 (시트 이름으로)
            ExcelWorksheet worksheet = package.Workbook.Worksheets["Overall"];
            worksheet = package.Workbook.Worksheets[worksheet.Cells[1, 1].Text];
            // 데이터 읽기 예제]
            int Paralength = int.Parse(worksheet.Cells[1, 2].Text);
            int rowCount = int.Parse(worksheet.Cells[1, 1].Text) * Paralength + 1;
            int colCount = worksheet.Dimension.Columns;
            Debug.Log(rowCount);
            Debug.Log(colCount);
            craftUIinfos = new CraftUIinfo[5];

            for (int row = 2; row <= rowCount; row += Paralength)
            {
                CraftUIinfo craftUIinfo = new CraftUIinfo(); // 각 루프에서 새로운 craftUIinfo 객체 생성
                craftUIinfo.name = worksheet.Cells[row, 1].Text; // 가장 첫번째 값에 접근해 이름 저장
                Debug.Log(craftUIinfo.name);
                craftUIinfo.imageName = worksheet.Cells[row, 2].Text; // 사진 이름 저장
                craftUIinfos[(row - 2) / Paralength] = craftUIinfo; // 데이터를 스캔할 때 Paralength만큼 띄우므로 그에 대한 보정 후 데이터 하나씩 저장

                string[] itemname = new string[20]; // 각 루프에서 새로운 itemname 배열 생성
                int[,] itemnum = new int[3, 11]; // 각 루프에서 새로운 itemnum 배열 생성

                for (int j = 0; j < colCount-1; j++)
                {
                    itemname[j] = worksheet.Cells[row + 1, j + 2].Text; // 그다음 줄에서 데이터 추출 - row=1일때는 필요없는 값
                    Debug.Log(itemname[j]);
                }
                Debug.Log("clear");
                for (int i = 2; i < Paralength; i++) // i에 따라 레벨 구분
                {
                    for (int j = 0; j < colCount-1; j++) // j에 따라 아이템 종류 구분
                    {
                        itemnum[i - 2, j] = int.Parse(worksheet.Cells[row + i, j+2].Text); // 배열 시작 위치가 약간 다르므로 보정
                        Debug.Log(itemnum[i - 2, j]);
                    }
                }

                Dataset_craft Precraft = new Dataset_craft();
                Precraft.item_name = itemname;
                Precraft.item_num = itemnum;
                craftRequests[(CraftTypes)Enum.Parse(typeof(CraftTypes), craftUIinfo.name)] = Precraft;
                Debug.Log(row);
            }
            Debug.Log(craftRequests[(CraftTypes)Enum.Parse(typeof(CraftTypes),"Floor")].item_num[0, 5]);
            _gameManager.GetCraftScrollviewManager().Content_Change(craftUIinfos);
        }
    }
}
