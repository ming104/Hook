using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappingHook : MonoBehaviour
{
    public LineRenderer line;
    public Transform hook;

    private Vector2 mousedir;

    public bool isHookActive;
    public bool isLineMax;
    public bool isAttach;
    
    // Start is called before the first frame update
    void Start()
    {
        line.positionCount = 2;
        line.endWidth = line.startWidth = 0.05f;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);
        line.useWorldSpace = true;
        isAttach = false;
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);

        if (Input.GetMouseButtonDown(0) && !isHookActive) // 갈고리 발사
        {
            hook.position = transform.position; // 갈고리 좌표를 현재 좌표로 지정
            mousedir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // 마우스 클릭 위치 즉 누른 위치를 받아옴
            isHookActive = true; // hook이 활성화 되어야 하니 키고
            isLineMax = false; // 첨 시작은 max가 아니니까 끄고
            hook.gameObject.SetActive(true); // hook GameObject를 켬
        }

        if (isHookActive && !isLineMax && !isAttach) // 훅이 활성화가 되었고 줄이 끝까지 안갔고 벽에 안 붙었으니까 
        {
            hook.Translate(mousedir.normalized * Time.deltaTime * 50); // 마우스 위치로 발사!
            if (Vector2.Distance(transform.position, hook.position) > 20) // 만약 캐릭터의 좌표와 hook의 최고 좌표가 5를 넘었다면
            {
                isLineMax = true; // 줄 최대치이니까 켬
            }
            if(Input.GetMouseButtonUp(0)) // 마우스에서 손때면
            {
                isHookActive = false; // 훅 꺼짐
                isLineMax = false; // 라인도 최대치가 아니게되니까 끔
                hook.gameObject.SetActive(false); // 게임오브젝트를 끔
            }
        }
        
        else if (isHookActive && isLineMax && !isAttach) // 훅은 켜졌고, 라인이 최대치이지만 벽에 안붙었으면?
        {
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * 40); // 다시 돌아감
            if (Vector2.Distance(transform.position, hook.position) < 0.1f) // 만약 플레이어랑 겹쳐졌으면 
            {
                isHookActive = false; // 훅을 끄고
                isLineMax = false; // 라인 맥스 초기화
                hook.gameObject.SetActive(false); // hook GameObject를 끔
            }
        }
        
        else if (isAttach) // 벽에 붙었다면
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 1.5f, ForceMode2D.Force);
            if(Input.GetMouseButtonUp(0)) // 마우스에서 손때면
            {
                isAttach = false; // 벽에 붙은거 없애고
                isHookActive = false; // 훅 꺼짐
                isLineMax = false; // 라인도 최대치가 아니게되니까 끔
                hook.GetComponent<Hook>().joint2D.enabled = false; // 벽에 붙었지만 이제는 풀어야 하니까 false해줌
                hook.gameObject.SetActive(false); // 게임오브젝트를 끔
            }
        }
    }
}
