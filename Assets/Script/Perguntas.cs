using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Xml.Linq;
using UnityEditor.Experimental.GraphView;

public class Perguntas: MonoBehaviour
{
    //PEGA API // Exemplo de URL para obter perguntas de múltipla escolha
    private string apiUrl = "https://opentdb.com/api.php?amount=10&category=15&difficulty=hard&type=boolean"; 

    //CRIA LISTA
    [System.Serializable]
    public class Question
    {
        public string question; //Cada questão 
        public string[] options; //As resposta
        public int correctOptionIndex;//Índice da resposta correta
    }

    //Controla a pontuação
    private int point = 0;
    public Text pointText; //Referência ao elemento Text onde você exibirá a pontuação
    public GameObject ObjQuestion;


    public List<Question> triviaQuestions = new List<Question>();
    public Text questionText; // Referência ao elemento Text onde você exibirá a pergunta
    public Text[] optionTexts; // Referência aos elementos Text onde você exibirá as opções

    private int currentQuestionIndex = 0; //índice atual da questão


    void Start()
    {
        ObjQuestion.SetActive(false);
        StartCoroutine(GetTriviaQuestions()); //Chama a rotina para start das perguntas
        pointText.text = point.ToString(); //Inicializa a pontuação em zero no elemento text

    }

    IEnumerator GetTriviaQuestions()
    {
        
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl)) //Se comunica com a API através da URL
        {
            yield return webRequest.SendWebRequest(); //Faz a requisição para a API

            if (webRequest.result == UnityWebRequest.Result.ConnectionError) //Exibe erro, em caso de erro
            {
                Debug.LogError("Erro de conexão: " + webRequest.error);
            }
            else if (webRequest.result == UnityWebRequest.Result.Success) //Realiza o tratamento das perguntas
            {
                string jsonText = webRequest.downloadHandler.text;

                // Parse JSON
                JSONNode jsonData = JSON.Parse(jsonText);
                JSONArray questionsArray = jsonData["results"].AsArray;

                // Limpar a lista de perguntas
                triviaQuestions.Clear();

                foreach (JSONNode questionData in questionsArray)
                {
                    Question newQuestion = new Question();
                    newQuestion.question = questionData["question"];
                    newQuestion.correctOptionIndex = Random.Range(0, 1); // Escolha uma opção correta aleatória
                    Debug.Log(newQuestion.question);
                    newQuestion.options = new string[2];
                    newQuestion.options[newQuestion.correctOptionIndex] = questionData["correct_answer"];

                    for (int i = 0, j = 0; i < 1; i++)
                    {
                        if (i != newQuestion.correctOptionIndex)
                        {
                            newQuestion.options[i] = questionData["incorrect_answers"][j];
                            j++;
                          

                        }
                    }

                    triviaQuestions.Add(newQuestion);
                    
                }
                
                NextQuestion();
            }
        }
    }

    void DisplayQuestion(Question question)
    {
        questionText.text = question.question;
 
        for (int i = 0; i < question.options.Length; i++)
        {
            optionTexts[i].text = question.options[i];
        }


    }

    public void NextQuestion()
    { 
        
        if (currentQuestionIndex < triviaQuestions.Count)
        {
            DisplayQuestion(triviaQuestions[currentQuestionIndex]);
        }

        currentQuestionIndex++;
    }


    public void CheckAnswer(int selectedOptionIndex)
    {
        if (currentQuestionIndex < triviaQuestions.Count)
        {

            if (selectedOptionIndex == triviaQuestions[currentQuestionIndex].correctOptionIndex)
            {
                point++;
                pointText.text = point.ToString();

            }

            if (currentQuestionIndex < triviaQuestions.Count)
            {

                NextQuestion();
            }
        }
        else
        {
            questionText.text = "FIM DE PERGUNTAS";
            ObjQuestion.SetActive(false);

        }
    }


}
