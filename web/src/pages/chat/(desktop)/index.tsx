import { ChatList, DraggablePanel, Tooltip, } from "@lobehub/ui";
import { Select } from 'antd';
import { useEffect, useState } from "react";
import { CreateChatDialog, CreateChatDialogHistory, DeleteDialog, DeleteDialogHistory, GetChatApplicationsList, GetChatDialog, GetChatDialogHistory, PutChatHistory } from "../../../services/ChatApplicationService";
import Divider from "@lobehub/ui/es/Form/components/FormDivider";
import { Button, message } from 'antd'
import { useNavigate } from "react-router-dom";
import styled from "styled-components";
import { DeleteOutlined } from '@ant-design/icons';
import {
    ActionIcon,
    ChatInputActionBar,
    ChatInputArea,
    ChatSendButton,
} from '@lobehub/ui';
import {
    ActionsBar,
    ChatListProps,
} from '@lobehub/ui';

import { Eraser, Languages } from 'lucide-react';
import { Flexbox } from 'react-layout-kit';
import { fetchRaw } from "../../../utils/fetch";
import CreateDialog from "../feautres/CreateDialog";

const DialogList = styled.div`
    margin-top: 8px;
    padding: 8px;
    overflow: auto;
    height: calc(100vh - 110px);
`;

const DialogItem = styled.div`
    padding: 8px;
    border: 1px solid #d9d9d9;
    border-radius: 8px;
    cursor: pointer;
    margin-bottom: 8px;
    transition: border-color 0.3s linear;
    &:hover {
        border-color: #1890ff;
    }

    // 当组件被选中时修改样式
    &.selected {
        border-color: #1890ff;
    }
`;

export default function DesktopLayout() {
    const navigate = useNavigate();
    const [applications, setApplications] = useState([] as any[]);
    const [application, setApplication] = useState(null as any);
    const [dialogs, setDialogs] = useState([] as any[]);
    const [createDialogVisible, setCreateDialogVisible] = useState(false);
    const [dialog, setDialog] = useState({} as any);
    const [history, setHistory] = useState([] as any[]);
    const [value, setValue] = useState('' as string);
    const [loading, setLoading] = useState(false);
    const [input] = useState({
        page: 1,
        pageSize: 5
    });

    async function loadingApplications() {
        try {
            const result = await GetChatApplicationsList(1, 1000);
            setApplications(result.result);
            if (result.total === 0) {
                message.error('您还没有应用，请先创建应用');
                // 等待1秒后跳转
                setTimeout(() => {
                    navigate('/app');
                }, 1000);
                return;
            }
            setApplication(result.result[0]);
        } catch (error) {

        }
    }

    async function loadingDialogs() {
        try {

            const result = (await GetChatDialog(application.id, true)) as any[];
            setDialogs(result);
            if (result.length === 0) {
                await AddChatDialog({
                    name: '默认对话',
                    description: '默认创建的对话',
                    applicationId: application.id,
                    type: 0
                })
                loadingDialogs();
                return;
            }
            setDialog(result[0]);
        } catch (error) {

        }
    }

    async function LoadingSession() {
        try {
            const result = await GetChatDialogHistory(dialog.id, input.page, input.pageSize);

            const history = result.result.map((item: any) => {
                return {
                    content: item.content,
                    createAt: item.createAt,
                    extra: {},
                    id: item.id,
                    meta: {
                        avatar: item.current ? "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg" : "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/chatgpt.png",
                        title: item.current ? "我" : "AI助手",
                    },
                    role: item.current ? 'user' : 'assistant',
                };
            });

            setHistory(history);

            // 等待1秒后滚动到底部
            setTimeout(() => {
                const chatlayout = document.getElementById('chat-layout');
                if (chatlayout) {
                    chatlayout.scrollTop = chatlayout.scrollHeight;
                }
            }, 1000);
        } catch (error) {

        }
    }

    async function AddChatDialog(data: any) {
        try {
            await CreateChatDialog(data)
        } catch (error) {
            message.error('创建失败');
        }
    }


    useEffect(() => {
        if (application) {
            loadingDialogs();
        }
    }, [application]);

    useEffect(() => {
        if (dialog) {
            LoadingSession();
        }
    }, [dialog, input]);

    useEffect(() => {
        loadingApplications();
    }, []);

    function handleChange(value: any) {
        console.log(`selected ${value}`);
    }

    const control: ChatListProps | any =
    {
        showTitle: false,
    }

    async function sendChat() {
        const v = value;
        if(loading){
            return
        }

        setLoading(true);
        setValue('');
        
        const chatlayout = document.getElementById('chat-layout');

        history.push({
            content: v,
            createAt: new Date().toISOString(),
            extra: {},
            id: new Date().getTime(),
            meta: {
                avatar: "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg",
                title: "我",
            },
            role: 'user',
        })

        setHistory([...history]);


        let chat = {
            content: '',
            createAt: new Date().toISOString(),
            extra: {},
            id: new Date().getTime(),
            meta: {
                avatar: "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/chatgpt.png",
                title: "AI助手",
            },
            role: 'assistant',
        };

        setHistory([...history, chat]);

        // 滚动到底部
        if (chatlayout) {
            chatlayout.scrollTop = chatlayout.scrollHeight;
        }

        const stream = await fetchRaw('/api/v1/ChatApplications/Completions', {
            chatDialogId: dialog.id,
            content: v,
            chatId: application.id
        });


        for await (const c of stream) {
            if (c) {
                let content = c;
                // 先匹配删除前缀 [ 和后缀 ]
                if (content.startsWith('[') || content.startsWith(',')) {
                    // 删除第一个字符
                    content = c.slice(1);
                }
                if (content.endsWith(']')) {
                    // 删除最后一个字符
                    content = c.slice(0, c.length - 1);
                }
                if (content === '') {
                    return;
                }

                if (content.startsWith(',') === true) {
                    content = content.slice(1);
                }

                content = "[" + content + "]";

                var obj = JSON.parse(content) as any[];

                obj.forEach((item) => {
                    chat.content += item.content;
                });

                setHistory([...history, chat]);

                // 滚动到底部
                if (chatlayout) {
                    chatlayout.scrollTop = chatlayout.scrollHeight;
                }
            }
        }

        await CreateChatDialogHistory({
            chatDialogId: dialog.id,
            content: v,
            current: true,
            type: 0
        })


        await CreateChatDialogHistory({
            chatDialogId: dialog.id,
            content: chat.content,
            current: false,
            type: 0
        })

        
        setLoading(false);
    }

    function deleteDialog(id: string) {
        DeleteDialog(id)
            .then(() => {
                loadingDialogs();
            })
    }

    async function ActionsClick(e: any, item: any) {
        if (e.key === 'del') {
            await DeleteDialogHistory(item.id)
            message.success('删除成功');

            const index = history.findIndex((i) => i.id === item.id);
            history.splice(index, 1);
            setHistory([...history]);

        } else if (e.key === 'regenerate') {
            message.error('暂时并未支持重置!');
        }
    }

    return <>
        <DraggablePanel
            mode="fixed"
            placement="left"
            showHandlerWhenUnexpand={true}
            resize={false}
            pin={true}
            minWidth={0}
        >
            <div
                style={{
                    padding: 8,
                }}>

                <div style={{
                    fontSize: 20,
                    fontWeight: 600,
                    marginBottom: 16
                }}>
                    请选择您的应用
                </div>
                <Select
                    title="请选择您的应用"
                    style={{
                        width: '100%',
                    }}
                    onChange={handleChange}
                    defaultValue={application?.id}
                    value={application?.id}
                    options={applications.map((item) => {
                        return { label: item.name, value: item.id }
                    })}
                />
            </div>
            <Divider />
            <DialogList>
                {
                    dialogs?.map((item: any) => {
                        return <DialogItem
                            key={item.id}
                            // 当组件被选中时修改样式
                            className={dialog?.id === item.id ? 'selected' : ''}
                            onClick={() => {
                                setDialog(item);
                            }}>
                            <Tooltip title={item.description}>
                                {item.name}
                            </Tooltip>
                            <Button
                                style={{
                                    float: 'inline-end',
                                }}
                                size='small'
                                icon={<DeleteOutlined />}
                                onClick={() => deleteDialog(item.id)}
                            />
                        </DialogItem>
                    })
                }
                <Button onClick={() => setCreateDialogVisible(true)} style={{
                    marginTop: 8
                }} block>新建对话</Button>

            </DialogList>
        </DraggablePanel>
        <div style={{
            height: '100vh',
            width: '100%',
        }}>
            <div style={{ height: 60 }}>
                <div style={{
                    fontSize: 20,
                    fontWeight: 600,
                    textAlign: 'left',
                    padding: 15
                }}>
                    {dialog.name}
                </div>
            </div>
            <Divider />
            <div id='chat-layout' style={{
                height: 'calc(100vh - 362px)',
                overflow: 'auto',
            }}>
                <ChatList
                    onMessageChange={async (e:any, message) => {
                        if(e === 0){
                            return
                        }
                        await PutChatHistory({
                            id: e,
                            content: message
                        })
                        // 修改history
                        history.forEach((item) => {
                            if (item.id === e) {
                                item.content = message;
                            }
                        });
                        setHistory([...history]);
                    }}
                    data={(history.length === 0 || history === null) ? [{
                        content: application?.opener??"",
                        createAt: new Date().toISOString(),
                        extra: {},
                        id: 0,
                        meta: {
                            avatar: "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/chatgpt.png",
                            title: "AI助手",
                        },
                        role: 'assistant',
                    }]: history}
                    onActionsClick={ActionsClick}
                    renderActions={ActionsBar}
                    renderMessages={{
                        default: ({ id, editableContent }) => <div id={id}>{editableContent}</div>,
                    }}
                    style={{ width: '100%' }}
                    {...control}
                />
            </div>
            <Divider />
            <div style={{ height: 300 }}>
                <Flexbox style={{ height: 300, position: 'relative' }}>
                    <ChatInputArea
                        value={value}
                        onChange={(e) => {
                            setValue(e.target.value);
                        }}
                        onKeyUpCapture={(e) => {

                            if (e.key === 'Enter' && !e.shiftKey && value !== '') {
                                sendChat();
                            }
                        }}
                        placeholder="请输入您的消息"
                        bottomAddons={<ChatSendButton loading={loading} onSend={() => sendChat()} />}
                        topAddons={
                            <ChatInputActionBar
                                leftAddons={
                                    <>
                                        <ActionIcon icon={Languages} color={undefined} fill={undefined} fillOpacity={undefined} fillRule={undefined} focusable={undefined} />
                                        <ActionIcon onClick={() => {
                                            setValue('');
                                        }} icon={Eraser} color={undefined} fill={undefined} fillOpacity={undefined} fillRule={undefined} focusable={undefined} />
                                    </>
                                }
                            />
                        }
                    />
                </Flexbox>
            </div>
            <CreateDialog visible={createDialogVisible} id={application?.id} type={0} onClose={() => {
                setCreateDialogVisible(false);
                loadingDialogs();
            }} onSucess={() => {
                setCreateDialogVisible(false);
                loadingDialogs();
            }} />
        </div>
    </>
}