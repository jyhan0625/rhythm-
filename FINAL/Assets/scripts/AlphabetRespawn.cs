﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class AlphabetRespawn : MonoBehaviour
{
    public GameObject[] alphabetPrefabs; // A, S, D ���ĺ� �̹��� ������ �迭
    public float spawnInterval = 1f; // ���ĺ� ���� �ֱ� (�� ����)
    public TextMeshProUGUI Score;
    public GameObject ScoreObject;
    public GameObject Play;
    public GameObject Credit;
    public AudioSource MainAudio;
    public AudioSource PlayAudio;
    private RectTransform canvasRectTransform; // Canvas�� RectTransform
    private float padding = 20f; // ȭ�� ��迡���� �е� ��
    private Coroutine startCoroutine;
    private GameObject newAlphabet;

    void OnEnable()
    {
        // 트리거가 true이면 코루틴 시작
         startCoroutine = StartCoroutine(SpawnAlphabet());
    }

    void OnDisable()
    {
        // 컴포넌트가 비활성화될 때 코루틴 중지
        if (startCoroutine != null)
        {
            StopCoroutine(startCoroutine);
            startCoroutine = null;
        }
    }

    void Start()
    {
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>(); // �θ� Canvas�� RectTransform ��������
    
    }

    IEnumerator SpawnAlphabet()
    {

        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // ���� ���ĺ� ����
            GameObject alphabetPrefab = GetRandomAlphabetPrefab();

            // ���ο� ���ĺ� �̹��� ����
            newAlphabet = Instantiate(alphabetPrefab, transform);

            // ���� ��ġ ����
            RectTransform rectTransform = newAlphabet.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = GetRandomPosition(rectTransform.sizeDelta);

            // ����� �Է��� ��ٸ�
            yield return WaitForInput(newAlphabet);

            // ����ڰ� ���ĺ��� ������ �ʰ� �ð��� ������ ���ĺ� ����
            Destroy(newAlphabet);
        }
    }

    GameObject GetRandomAlphabetPrefab()
    {
        // A, S, D �� �����ϰ� ����
        int randomIndex = Random.Range(0, 3);
        return alphabetPrefabs[randomIndex];
    }

    Vector2 GetRandomPosition(Vector2 size)
    {
        float x = Random.Range(canvasRectTransform.rect.width - 2300, canvasRectTransform.rect.width - 1000);
        float y = Random.Range(canvasRectTransform.rect.height - 1300, canvasRectTransform.rect.height - 600);
        return new Vector2(x, y);
    }

    IEnumerator WaitForInput(GameObject alphabet)
    {
        bool keyPressed = false;
        while (!keyPressed)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {

                if (alphabet.name == "A(Clone)") Score.text = (int.Parse(Score.text) + 1).ToString();
                else Score.text = (int.Parse(Score.text) - 1).ToString();

                keyPressed = true;
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {

                if (alphabet.name == "S(Clone)") Score.text = (int.Parse(Score.text) + 1).ToString();
                else Score.text = (int.Parse(Score.text) - 1).ToString();
                keyPressed = true;
            }

            else if (Input.GetKeyDown(KeyCode.D))
            {

                if (alphabet.name == "D(Clone)") Score.text = (int.Parse(Score.text) + 1).ToString();
                else Score.text = (int.Parse(Score.text) - 1).ToString();
                keyPressed = true;
            }

            if (int.Parse(Score.text) == -1)
            {
                Score.text = "0";
                Destroy(newAlphabet);
                DeActivate();
            }


            yield return null;
        }
    }

    public void Activate()
    {
        this.enabled = true;
        Debug.Log("AlphabetRespawn has been activated!");
    }

    public void DeActivate()
    {
        this.enabled = false;
        ScoreObject.SetActive(false);
        Play.SetActive(true);
        Credit.SetActive(true);
        MainAudio.Play();
        PlayAudio.Stop();
        Debug.Log("AlphabetRespawn has been deactivated!");
    }
}
