using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class JournalGameManager : MonoBehaviour
{
    public Questions[] questions;
    private static List<Questions> unanswered;

    private Questions current;
    
    [SerializeField]
    private Text factText;

    [SerializeField]
    private Text trueAnswer;

    [SerializeField]
    private Text falseAnswer;

    [SerializeField]
    private Animator Anim;

    int count = 0;

    [SerializeField]
    private float timebetween = 1f;
    private void Start()
    {
        if (unanswered == null || unanswered.Count == 0)
        {
            unanswered = questions.ToList<Questions>();
        }

        GetQuestion();

        if (count == 7)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }

    void GetQuestion()
    {
        current = unanswered[count];

        factText.text = current.fact;
        count++;

        if(current.isTrue)
        {
            trueAnswer.text = "CORRECT";
            falseAnswer.text = "WRONG";
        }
        else
        {
            trueAnswer.text = "WRONG";
            falseAnswer.text = "CORRECT";
        }
    }

    IEnumerator TransitionToNextQuestion()
    {
        unanswered.Remove(current);

        yield return new WaitForSeconds(timebetween);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       

    }

    public void UserSelectTrue()
    {
        Anim.SetTrigger("True");
        if (current.isTrue)
        {
            Debug.Log("CORRECT");
        }
        else
        {
            Debug.Log("FALSE");
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse ()
    {
        Anim.SetTrigger("False");
        if (!(current.isTrue))
        {
            Debug.Log("CORRECT");
        }
        else
        {
            Debug.Log("FALSE");
        }
        StartCoroutine(TransitionToNextQuestion());

    }

}
